using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace evojacu.Endpoints.Kategorija.Update
{
    [Route("Kategorija-update")]
    public class KategorijaUpdateEndpoint : MyBaseEndpoint<KategorijaUpdateRequest, KategorijaUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public KategorijaUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<KategorijaUpdateResponse> Obradi([FromBody] KategorijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var kategorije = _applicationDbContext.Kategorije.FirstOrDefault(x => x.KategorijaID == request.KategorijaID);

            if (kategorije == null)
            {
                throw new Exception("Ne postoji kategorija sa ID= " + request.KategorijaID);
            }

            kategorije.Naziv = request.Naziv;

            if (!string.IsNullOrEmpty(request.Slika_base64_format))
            {
                byte[]? slika_bajtovi = request.Slika_base64_format?.ParsirajBase64();

                if (slika_bajtovi == null)
                    throw new Exception("pogresan base64 format");

                byte[]? slika_bajtovi_resized_velika = resize(slika_bajtovi, 200);
                if (slika_bajtovi_resized_velika == null)
                    throw new Exception("pogresan format slike");

                byte[]? slika_bajtovi_resized_mala = resize(slika_bajtovi, 50);
                if (slika_bajtovi_resized_mala == null)
                    throw new Exception("pogresan format slike");

                var folderPath = "slike-kategorija";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileNameVelika = $"{folderPath}/{kategorije.KategorijaID}-velika.jpg";
                var fileNameMala = $"{folderPath}/{kategorije.KategorijaID}-mala.jpg";

                await System.IO.File.WriteAllBytesAsync(fileNameVelika, slika_bajtovi_resized_velika, cancellationToken);
                await System.IO.File.WriteAllBytesAsync(fileNameMala, slika_bajtovi_resized_mala, cancellationToken);
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KategorijaUpdateResponse { KategorijaID = kategorije.KategorijaID };
        }

        private byte[]? resize(byte[] slika_bajtovi, int newMaxSize)
        {
            try
            {
                using (SKBitmap original = SKBitmap.Decode(slika_bajtovi))
                {
                    int newWidth, newHeight;

                    if (original.Width > original.Height)
                    {
                        newWidth = newMaxSize;
                        newHeight = original.Height * newMaxSize / original.Width;
                    }
                    else
                    {
                        newHeight = newMaxSize;
                        newWidth = original.Width * newMaxSize / original.Height;
                    }


                    using (SKBitmap resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High))
                    using (SKImage image = SKImage.FromBitmap(resized))
                    using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 80))
                    {
                        return data.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
