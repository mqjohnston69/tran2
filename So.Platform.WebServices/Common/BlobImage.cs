#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
#endregion

namespace Oc.Carbon.WebServices.Common
{
    class VersionData
    {
        public string accountKey { get; set; }
        public string container { get; set; }
    }

    class BlobImage
    {

        #region Constants

        private const string METADATA_CAPTION_KEY = "Caption";
        private const string METADATA_DESCRIPTION_KEY = "Description";
        private const string METADATA_UPLOADKEY_KEY = "UploadKey";
        private const string METADATA_CUSTOMER_KEY = "CustomerID";
        private const string METADATA_ORIGINAL_FILENAME = "FileName";


        #endregion

        #region Properties
        public string Url { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string UploadKey { get; set; }
        public string CustomerID { get; set; }

        //static public string LocalRoot = @"C:\ScanOptics\LocalStorage\";
        #endregion

        #region Methods
        /// <summary>
        /// Connect to blob storage.
        /// </summary>
        /// <returns></returns>
        private static CloudBlobContainer getCloudBlobContainer(VersionData VersionInfo)
        {
            //Directory.CreateDirectory(LocalRoot);
            string accountKey = VersionInfo.accountKey;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);
            CloudBlobClient storageClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer sampleContainer = storageClient.GetContainerReference(VersionInfo.container);
            sampleContainer.CreateIfNotExists();

            return sampleContainer;
        }

        /// <summary>
        /// Delete an image.
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteImage(string fileName, VersionData VersionInfo)
        {
            var sampleContainer = getCloudBlobContainer(VersionInfo);

            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(fileName);

            blob.Delete();
        }

        /// <summary>
        /// Upload an image to the blob
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="imageStream"></param>
        /// <param name="UploadKey"></param>
        /// <param name="CustomerID"></param>
        public static bool UploadImage(string fileName, byte[] binData, string storageKey, VersionData VersionInfo, string OrgKey, string InputFile)
        {
            var sampleContainer = getCloudBlobContainer(VersionInfo);

            if (sampleContainer != null)
            {
                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(storageKey);
                if (blob != null)
                {
                    if (!blob.Metadata.ContainsKey(METADATA_CAPTION_KEY)) blob.Metadata[METADATA_CAPTION_KEY] = fileName;
                    if (!blob.Metadata.ContainsKey(METADATA_DESCRIPTION_KEY)) blob.Metadata[METADATA_DESCRIPTION_KEY] = "Image uploaded at " + DateTime.Now.ToString();
                    if (!blob.Metadata.ContainsKey(METADATA_UPLOADKEY_KEY)) blob.Metadata[METADATA_UPLOADKEY_KEY] = storageKey;
                    if (!blob.Metadata.ContainsKey(METADATA_CUSTOMER_KEY)) blob.Metadata[METADATA_CUSTOMER_KEY] = OrgKey;
                    if (!blob.Metadata.ContainsKey(METADATA_ORIGINAL_FILENAME)) blob.Metadata[METADATA_ORIGINAL_FILENAME] = InputFile;
                    blob.UploadFromByteArray(binData,0,binData.Length);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get a list of all images in the blob.
        /// </summary>
        /// <returns></returns>
        public static List<BlobImage> LoadAllImages(VersionData VersionInfo)
        {
            var result = new List<BlobImage>();
            var sampleContainer = getCloudBlobContainer(VersionInfo);

            foreach (var blob in sampleContainer.ListBlobs(blobListingDetails: BlobListingDetails.Metadata))
            {
                CloudBlockBlob blockBlob = blob as CloudBlockBlob;
                if (blockBlob != null)
                {
                    string imageFile = System.IO.Path.GetFileName(blockBlob.Uri.ToString());
                    if (!blockBlob.Metadata.ContainsKey(METADATA_CAPTION_KEY)) blockBlob.Metadata[METADATA_CAPTION_KEY] = imageFile;
                    if (!blockBlob.Metadata.ContainsKey(METADATA_DESCRIPTION_KEY)) blockBlob.Metadata[METADATA_DESCRIPTION_KEY] = "Uploaded image";
                    if (!blockBlob.Metadata.ContainsKey(METADATA_UPLOADKEY_KEY)) blockBlob.Metadata[METADATA_UPLOADKEY_KEY] = string.Empty;
                    if (!blockBlob.Metadata.ContainsKey(METADATA_CUSTOMER_KEY)) blockBlob.Metadata[METADATA_CUSTOMER_KEY] = string.Empty;
                    if (!blockBlob.Metadata.ContainsKey(METADATA_ORIGINAL_FILENAME)) blockBlob.Metadata[METADATA_ORIGINAL_FILENAME] = string.Empty;

                    result.Add(new BlobImage()
                    {
                        Caption = blockBlob.Metadata[METADATA_CAPTION_KEY],
                        Description = blockBlob.Metadata[METADATA_DESCRIPTION_KEY],
                        UploadKey = blockBlob.Metadata[METADATA_UPLOADKEY_KEY],
                        CustomerID = blockBlob.Metadata[METADATA_CUSTOMER_KEY],
                        FileName = blockBlob.Metadata[METADATA_ORIGINAL_FILENAME],
                        Url = blockBlob.Uri.ToString()
                    });
                }
                //blockBlob.Delete();
            }

            return result;
        }

        /// <summary>
        /// Get an image from the blob, does not delete it.
        /// Allows the caller to specify where to store the local file.
        /// </summary>
        /// <param name="BlobFileName"></param>
        /// <param name="LocalFileName"></param>
        /// <returns></returns>
        internal static IList<byte[]> GetImage(string BlobFileName, VersionData VersionInfo,int imageType)
        {
            var sampleContainer = getCloudBlobContainer(VersionInfo);

            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(BlobFileName);
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                IList<byte[]> images = new List<byte[]>();
                if (imageType == 2)
                {
                    images = GetTiffImageThumb(memoryStream, 120, 200);
                }
                else
                {
                    images.Add(memoryStream.ToArray());
                    //images = PDFToImage(memoryStream, 120, 200);
                }
                return images;
            }
        }
        #endregion

        public static IList<byte[]> GetTiffImageThumb(MemoryStream ms, int ImgWidth, int ImgHeight)
        {

            Image SrcImg = null;
            Image returnImage = null;
            IList<byte[]> images = new List<byte[]>();
            try
            {
                SrcImg = Image.FromStream(ms);
                FrameDimension FrDim = new FrameDimension(SrcImg.FrameDimensionsList[0]);
                var pageCount = SrcImg.GetFrameCount(FrameDimension.Page);
                for (int page = 0; page < pageCount; page++)
                {

                    SrcImg.SelectActiveFrame(FrameDimension.Page, page);
                    MemoryStream byteStream = new MemoryStream();
                    SrcImg.Save(byteStream, ImageFormat.Jpeg);

                    // and then create a new Image from it
                    images.Add(byteStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SrcImg.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return images;
        }

        public static IList<byte[]> PDFToImage(MemoryStream ms, int ImgWidth, int ImgHeight)
        {

            string path = Path.GetDirectoryName(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);

            Ghostscript.NET.Rasterizer.GhostscriptRasterizer rasterizer = null;
            Ghostscript.NET.GhostscriptVersionInfo vesion = new Ghostscript.NET.GhostscriptVersionInfo(new Version(0, 0, 0),  @"C:\\Program Files\\gs\\gs9.19\\bin\\gsdll64.dll", string.Empty, Ghostscript.NET.GhostscriptLicense.GPL);
            IList <byte[]> images = new List<byte[]> ();
            using (rasterizer = new Ghostscript.NET.Rasterizer.GhostscriptRasterizer())
            {
                rasterizer.Open(ms, vesion, false);

                for (int i = 1; i <= rasterizer.PageCount; i++)
                {

                    MemoryStream returnImage = new MemoryStream();
                    Image SrcImg = rasterizer.GetPage(200, 200, i);
                    //if (SrcImg.Width <= ImgWidth) ImgWidth = SrcImg.Width;
                    //int NewHeight = SrcImg.Height * ImgWidth / SrcImg.Width;
                    //if (NewHeight > ImgHeight)
                    //{
                    //    // Resize with height instead
                    //    ImgWidth = SrcImg.Width * ImgHeight / SrcImg.Height;
                    //    NewHeight = ImgHeight;
                    //}

                    SrcImg.Save(returnImage, ImageFormat.Jpeg);
                    images.Add(returnImage.ToArray());
                }

                rasterizer.Close();
                return images;
            }
        }



    }


}
