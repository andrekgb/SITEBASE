using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Framework.Util
{
    /// <summary>
    /// Classe auxiliar para tratar imagens.
    /// </summary>
    public class Imagem
    {
        #region "Metodos"

        /// <summary>
        /// Retorna uma imagem, com fundo branco.
        /// </summary>
        /// <param name="imagem"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap adicionarFundoBranco(System.Drawing.Image imagem, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            using (var canvas = Graphics.FromImage(bitmap))
            {
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
                canvas.DrawImage(imagem, (int)((width - imagem.Width) / 2), (int)((height - imagem.Height) / 2), imagem.Width, imagem.Height);
                canvas.Save();
            }

            return bitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcFilename"></param>
        /// <param name="lnWidth"></param>
        /// <param name="lnHeight"></param>
        /// <returns></returns>
        public Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            Bitmap loBMP = new Bitmap(lcFilename);
            return CreateThumbnail(loBMP, lnWidth, lnHeight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="lnWidth"></param>
        /// <param name="lnHeight"></param>
        /// <returns></returns>
        public Bitmap CreateThumbnail(Stream stream, int lnWidth, int lnHeight)
        {
            Bitmap loBMP = new Bitmap(stream);
            return CreateThumbnail(loBMP, lnWidth, lnHeight);
        }

        /// <summary>
        /// Creates a resized bitmap from an existing image on disk.
        /// Call Dispose on the returned Bitmap object.
        /// </summary>
        /// <param name="lcFilename"></param>
        /// <param name="lnWidth"></param>
        /// <param name="lnHeight"></param>
        /// <returns>Bitmap or null</returns>
        public Bitmap CreateThumbnail(Bitmap loBMP, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;

            try
            {
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;


                //If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                //Redimensiona
                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                //Valida novamente se o redimensionamento ultrapassou os limites da imagem
                if (lnNewWidth > lnWidth)
                {
                    lnRatio = (decimal)lnWidth / lnNewWidth;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = lnNewHeight * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else if (lnNewHeight > lnHeight)
                {
                    lnRatio = (decimal)lnHeight / lnNewHeight;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = lnNewWidth * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                //This code creates cleaner (though bigger) thumbnails and properly
                //and handles GIF files better by generating a white background for
                //transparent images (as opposed to black)
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }

        /// <summary>
        /// Cria um thumbnail com base no width.
        /// </summary>
        /// <param name="lcFilename"></param>
        /// <param name="lnWidth"></param>
        /// <param name="lnHeight"></param>
        /// False: Valida somente largura;</param>
        /// <returns>Bitmap or null</returns>
        public Bitmap CreateThumbnailWidthBased(Bitmap loBMP, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;

            try
            {
                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;


                //If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth)
                    return loBMP;

                lnRatio = (decimal)lnWidth / loBMP.Width;
                lnNewWidth = lnWidth;
                decimal lnTemp = loBMP.Height * lnRatio;
                lnNewHeight = (int)lnTemp;

                //This code creates cleaner (though bigger) thumbnails and properly
                //and handles GIF files better by generating a white background for
                //transparent images (as opposed to black)
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }

        /// <summary>
        /// Redimensiona uma imagem.
        /// </summary>
        /// <param name="streamImagem">Stream.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public System.Drawing.Image redimensionaImagem(Stream streamImagem, int width, int height)
        {
            System.Drawing.Image imagem = System.Drawing.Image.FromStream(streamImagem);

            return redimensionaImagem(imagem, width, height);
        }

        /// <summary>
        /// Redimensiona uma imagem.
        /// </summary>
        /// <param name="imagem">Arquivo de imagem.</param>
        /// <param name="width">Largura da imagem.</param>
        /// <param name="height">Altura da imagem.</param>
        /// <returns></returns>
        public System.Drawing.Image redimensionaImagem(System.Drawing.Image imagem, int width, int height)
        {
            //Redimensiona se a imagem for maior que a altura e largura definida 
            if (isTamanhoValido(imagem, width, height, 0, 0) == 3)
            {
                float w_ = 0;
                float h_ = 0;

                if (width <= 0)
                {
                    width = imagem.Width;
                }

                if (height <= 0)
                {
                    height = imagem.Height;
                }

                if (imagem.Width > width)
                {
                    //Trata largura
                    w_ = width;
                    h_ = imagem.Height * ((float)width / imagem.Width);

                    //Trata altura
                    if (h_ > height)
                    {
                        w_ = width * ((float)height / h_);
                        h_ = height;
                    }
                }
                else if (imagem.Height > height)
                {
                    //Trata altura
                    h_ = height;
                    w_ = imagem.Width * ((float)height / imagem.Height);

                    //Trata largura
                    if (w_ > width)
                    {
                        h_ = height * ((float)width / w_);
                        w_ = width;
                    }
                }

                System.Drawing.Image imagemRedimensionada = new System.Drawing.Bitmap((int)w_, (int)h_, imagem.PixelFormat);

                System.Drawing.Graphics oGraphic = System.Drawing.Graphics.FromImage(imagemRedimensionada);
                oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                System.Drawing.Rectangle oRectangle = new System.Drawing.Rectangle(0, 0, (int)w_, (int)h_);
                oGraphic.DrawImage(imagem, oRectangle);

                oGraphic.Dispose();
                //imagemRedimensionada.Dispose()
                imagem.Dispose();

                return imagemRedimensionada;
            }
            else
            {
                return imagem;
            }
        }

        /// <summary>
        /// Valida se o tamanho da imagem é válido de acordo com a largura e altura informada.
        /// </summary>
        /// <param name="streamImagem">Stream da imagem.</param>
        /// <param name="imagem">Arquivo da imagem.</param>
        /// <param name="max_width">Largura máxima.</param>
        /// <param name="max_height">Altura máxima.</param>
        /// <param name="min_width">Largura mínima.</param>
        /// <param name="min_height">Altura mínima.</param>
        /// <returns></returns>
        public int isTamanhoValido(Stream streamImagem, int max_width, int max_height, int min_width, int min_height)
        {
            Image imagem = System.Drawing.Image.FromStream(streamImagem);
            return isTamanhoValido(imagem, max_width, max_height, min_width, min_height);
        }

        /// <summary>
        /// Valida se o tamanho da imagem é válido de acordo com a largura e altura informada.
        /// </summary>
        /// <param name="imagem">Arquivo da imagem.</param>
        /// <param name="max_width">Largura máxima.</param>
        /// <param name="max_height">Altura máxima.</param>
        /// <param name="min_width">Largura mínima.</param>
        /// <param name="min_height">Altura mínima.</param>
        /// <returns></returns>
        public int isTamanhoValido(System.Drawing.Image imagem, int max_width, int max_height, int min_width, int min_height)
        {
            if ((imagem.Width < min_width && min_width > 0) || (imagem.Height < min_height && min_height > 0))
                return 2;
            else if ((imagem.Width > max_width && max_width > 0) || (imagem.Height > max_height && max_height > 0))
                return 3;
            else
                return 1;
        }


        /// <summary>
        /// Maximiza a area para o crop de uma imagem de acordo com o tamanho da imagem e o tamanho do thumb.
        /// Width e Height são passados por referência.
        /// </summary>
        /// <param name="width">Width final da área.</param>
        /// <param name="height">Height final da área.</param>
        /// <param name="width_original">Width da imagem original.</param>
        /// <param name="height_original">Height da imagem original.</param>
        /// <param name="width_thumb">Width da thumb.</param>
        /// <param name="height_thumb">Height da thumb.</param>
        /// <param name="proporcao">Proporção da imagem original que a thumb deve pegar.</param>
        public void getImageCropArea(out int width, out int height, int width_original, int height_original,
                                    int width_thumb, int height_thumb, int proporcao)
        {
            //Ajusta proporção
            if (proporcao < 1 || proporcao > 100)
                proporcao = 100;

            //Ajusta tamanho do thumb
            if (width_thumb > width_original)
                width_thumb = width_original;
            if (height_thumb > height_original)
                height_thumb = height_original;

            //Largura e altura maxima que a thumb pode chegar
            int thumb_area_w = width_original * proporcao / 100;
            int thumb_area_h = height_original * proporcao / 100;

            //Ajusta a area para nao ser menor que o tamanho da thumb
            if (width_thumb > thumb_area_w || height_thumb > thumb_area_h)
            {
                thumb_area_w = width_thumb;
                thumb_area_h = height_thumb;
            }

            //Ajusta o tamanho da area do crop de acordo com as proporções da thumb
            width = (int)thumb_area_w;
            height = (int)((float)thumb_area_w * ((float)height_thumb / (float)width_thumb));

            if (height > thumb_area_h)
            {
                height = thumb_area_h;
                width = (int)((float)thumb_area_h * ((float)width_thumb / (float)height_thumb));
            }
        }

        /// <summary>
        /// Exclui imagem do site.
        /// </summary>
        /// <param name="pasta"></param>
        /// <param name="nome"></param>
        public void excluiImagemSite(String imagem)
        {
            System.IO.File.Delete(imagem);
        }

        /// <summary>
        /// Retorna o tipo do codec pelo nome da imagem.
        /// </summary>
        /// <param name="imagem">Nome da imagem.</param>
        /// <returns>ImageCodecInfo.</returns>
        public ImageCodecInfo getEncoderInfoByFileName(string imagem)
        {
            return this.getEncoderInfo(this.getMimeType(imagem));
        }


        /// <summary>
        /// Retorna o tipo do codec pelo mimeType da imagem.
        /// </summary>
        /// <param name="mimeType">MimeType da imagem. Exemplo: image/jpeg.</param>
        /// <returns>ImageCodecInfo.</returns>
        /// <remarks></remarks>
        public ImageCodecInfo getEncoderInfo(string mimeType)
        {
            //Todos os codecs para cada tipo de imagem
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            //Seleciona o codec pelo mimeType
            for (int i = 0; i <= codecs.Length - 1; i++)
            {
                if ((codecs[i].MimeType.Equals(mimeType)))
                {
                    return codecs[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Retorna formato de imagem..
        /// </summary>
        /// <param name="imagem">Nome da imagem.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.Drawing.Imaging.ImageFormat getImageFormat(string imagem)
        {
            string extensao = System.IO.Path.GetExtension(imagem).ToLower();

            if (extensao.Equals(".jpg") || extensao.Equals(".jpeg"))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            else if (extensao.Equals(".gif"))
                return System.Drawing.Imaging.ImageFormat.Gif;
            else if (extensao.Equals(".png"))
                return System.Drawing.Imaging.ImageFormat.Png;
            else if (extensao.Equals(".bmp"))
                return System.Drawing.Imaging.ImageFormat.Bmp;

            return null;
        }

        /// <summary>
        /// Retorna o mimetype de uma imagem.
        /// </summary>
        /// <param name="imagem">Nome da imagem.</param>
        /// <returns>MimeType da imagem.</returns>
        /// <remarks></remarks>
        public string getMimeType(string imagem)
        {
            string extensao = System.IO.Path.GetExtension(imagem).ToLower();
            string mimeType = "";

            if (extensao.Equals(".jpg") || extensao.Equals(".jpeg"))
            {
                mimeType = "image/jpeg";
            }
            else if (extensao.Equals(".gif"))
            {
                mimeType = "image/gif";
            }
            else if (extensao.Equals(".png"))
            {
                mimeType = "image/png";
            }
            else if (extensao.Equals(".bmp"))
            {
                mimeType = "image/bmp";
            }

            return mimeType;
        }

        /// <summary>
        /// Retorna os parametros para utilizar o codec ao salvar uma imagem.
        /// </summary>
        /// <param name="qualidade">Qualidade da imagem. 0 a 100.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EncoderParameters getEncoderParams(int qualidade)
        {
            EncoderParameter qualityParam = default(EncoderParameter);

            if (qualidade > 0 & qualidade <= 100)
            {
                qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualidade);
            }
            else
            {
                qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);
            }

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            return encoderParams;
        }

        /// <summary>
        /// Faz o crop de uma imagem.
        /// </summary>
        /// <param name="imgPhoto">Arquivo da imagem.</param>
        /// <param name="Width">Largura da imagem.</param>
        /// <param name="Height">Altura da imagem.</param>
        /// <param name="posicao">Posição do crop</param>
        /// <returns>Imagem com crop.</returns>
        /// <remarks></remarks>
        public System.Drawing.Bitmap AutoCrop(System.Drawing.Image imgPhoto, Double Width, Double Height, TypePosicaoCropImagem posicao)
        {
            double sourceWidth = imgPhoto.Width;
            double sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            double nPercent = 0;
            double nPercentW = 0;
            double nPercentH = 0;

            nPercentW = ((double)Width / (double)sourceWidth);
            nPercentH = ((double)Height / (double)sourceHeight);

            if ((nPercentH < nPercentW))
            {
                nPercent = nPercentW;
                destY = 0;
                destY = ((int)(Height - (sourceHeight * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentH;
                destX = 0;
                destX = ((int)(Width - (sourceWidth * nPercent)) / 2);
            }

            //Posicionamento do crop
            switch (posicao)
            {
                case TypePosicaoCropImagem.LefTop:
                    destX = 0;
                    destY = 0;
                    break;
                case TypePosicaoCropImagem.CenterTop:
                    destX = ((int)(Width - (sourceWidth * nPercent)) / 2);
                    destY = 0;
                    break;
                case TypePosicaoCropImagem.RightTop:
                    destX = (int)(Width - (sourceWidth * nPercent));
                    destY = 0;
                    break;
                case TypePosicaoCropImagem.LeftMiddle:
                    destX = 0;
                    destY = ((int)(Height - (sourceHeight * nPercent)) / 2);
                    break;
                case TypePosicaoCropImagem.RightMiddle:
                    destX = (int)(Width - (sourceWidth * nPercent));
                    destY = ((int)(Height - (sourceHeight * nPercent)) / 2);
                    break;
                case TypePosicaoCropImagem.LeftBottom:
                    destX = 0;
                    destY = (int)(Height - (sourceHeight * nPercent));
                    break;
                case TypePosicaoCropImagem.CenterBottom:
                    destX = ((int)(Width - (sourceWidth * nPercent)) / 2);
                    destY = (int)(Height - (sourceHeight * nPercent));
                    break;
                case TypePosicaoCropImagem.RightBottom:
                    //Right Bottom
                    destX = (int)(Width - (sourceWidth * nPercent));
                    destY = (int)(Height - (sourceHeight * nPercent));
                    break;
                default:
                    //4 Center Middle
                    destX = ((int)(Width - (sourceWidth * nPercent)) / 2);
                    destY = ((int)(Height - (sourceHeight * nPercent)) / 2);
                    break;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap((int)Width, (int)Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        #endregion
    }
}
