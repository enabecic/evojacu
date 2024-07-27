using evojacu.Helpers;
using evojacu.Models;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace evojacu.Endpoints.Zadatak.Dodaj
{
    [Route("Zadatak-dodaj")]
    public class ZadatakDodajEndpoint : MyBaseEndpoint<ZadatakDodajRequest, ZadatakDodajResponse>
    {
        private readonly evojacuDBContext _applicationDbContext;

        public ZadatakDodajEndpoint(evojacuDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpPost]
        public override async Task<ZadatakDodajResponse> Obradi([FromBody]ZadatakDodajRequest request, CancellationToken cancellationToken = default)
        {
            var noviObj = new evojacu.Models.Zadatak
            {
                Opis = request.Opis,
                Naziv=request.Naziv,
                KategorijaID=request.KategorijaID
                
            };

            _applicationDbContext.Add(noviObj);
            await _applicationDbContext.SaveChangesAsync();

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
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(folderPath);
                }

                // Koristite novi ID objekta nakon sto je sacuvan
                var fileNameVelika = $"{folderPath}/{noviObj.ZadatakId}-velika.jpg";
                var fileNameMala = $"{folderPath}/{noviObj.ZadatakId}-mala.jpg";

                await System.IO.File.WriteAllBytesAsync(fileNameVelika, slika_bajtovi_resized_velika, cancellationToken);
                await System.IO.File.WriteAllBytesAsync(fileNameMala, slika_bajtovi_resized_mala, cancellationToken);
            }

            return new ZadatakDodajResponse
            {
                ZadatakId = noviObj.ZadatakId
            };
        }

        public static byte[]? resize(byte[] slikaBajtovi, int size, int quality = 75)
        {
            using var input = new MemoryStream(slikaBajtovi);
            using var inputStream = new SKManagedStream(input);
            using var original = SKBitmap.Decode(inputStream);
            int width, height;
            if (original.Width > original.Height)
            {
                width = size;
                height = original.Height * size / original.Width;
            }
            else
            {
                width = original.Width * size / original.Height;
                height = size;
            }

            using var resized = original.Resize(new SKImageInfo(width, height), SKBitmapResizeMethod.Lanczos3);
            if (resized == null) return null;

            using var image = SKImage.FromBitmap(resized);
            return image.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
        }
    }
}
