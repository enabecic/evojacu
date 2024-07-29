using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace evojacu.Endpoints.Zadatak.Update
{
    [Route("Zadatak-update")]
    public class ZadatakUpdateEndpoint : MyBaseEndpoint<ZadatakUpdateRequest, ZadatakUpdateResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakUpdateEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost]
        public override async Task<ZadatakUpdateResponse> Obradi([FromBody]ZadatakUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var zadatak = _applicationDbContext.Zadaci.FirstOrDefault(x => x.ZadatakId == request.ZadatakId);

            if (zadatak == null)
            {
                throw new Exception("Ne postoji zadatak sa ID = " + request.ZadatakId);
            }

            zadatak.Opis = request.Opis;
            zadatak.Naziv=request.Naziv;

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

                var folderPath = "slike-zadatak";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileNameVelika = $"{folderPath}/{zadatak.ZadatakId}-velika.jpg";
                var fileNameMala = $"{folderPath}/{zadatak.ZadatakId}-mala.jpg";

                await System.IO.File.WriteAllBytesAsync(fileNameVelika, slika_bajtovi_resized_velika, cancellationToken);
                await System.IO.File.WriteAllBytesAsync(fileNameMala, slika_bajtovi_resized_mala, cancellationToken);
            }

            await _applicationDbContext.SaveChangesAsync();


            return new ZadatakUpdateResponse
            {
                ZadatakId = zadatak.ZadatakId
            };
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
