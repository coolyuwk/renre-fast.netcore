using RenRen.Fast.Api.Entity;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysCaptchaServiceImpl : ISysCaptchaService
    {
        public readonly PassportDbContext _passportDbContext;

        public SysCaptchaServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        private byte[] createCaptcha(char[] code)
        {
            Color[] Colors = { Color.Black, Color.Red, Color.DeepPink, Color.Green };
            int Width = 200;
            int Height = 50;

            Random r = new Random();
            using Image<Rgba32> image = new Image<Rgba32>(Width, Height);

            // 字体
            Font font = SystemFonts.CreateFont(SystemFonts.Families.First().Name, 25, FontStyle.Bold);

            image.Mutate(ctx =>
            {
                // 白底背景
                ctx.Fill(Color.White);

                // 画验证码
                for (int i = 0; i < code.Length; i++)
                {
                    ctx.DrawText(code[i].ToString()
                        , font
                        , Colors[r.Next(Colors.Length)]
                        , new PointF(40 * i, r.Next(2, 12)));
                }

                // 画干扰线
                for (int i = 0; i < 10; i++)
                {
                    Pen pen = new Pen(Colors[r.Next(Colors.Length)], 1);
                    PointF p1 = new PointF(r.Next(Width), r.Next(Height));
                    PointF p2 = new PointF(r.Next(Width), r.Next(Height));

                    ctx.DrawLines(pen, p1, p2);
                }

                // 画噪点
                for (int i = 0; i < 80; i++)
                {
                    Pen pen = new Pen(Colors[r.Next(Colors.Length)], 1);
                    PointF p1 = new PointF(r.Next(Width), r.Next(Height));
                    PointF p2 = new PointF(p1.X + 1f, p1.Y + 1f);

                    ctx.DrawLines(pen, p1, p2);
                }
            });

            using System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.SaveAsJpeg(ms);
            return ms.ToArray();
        }


        public async Task<byte[]> GetCaptchaAsync(string uuid)
        {
            string code = Guid.NewGuid().ToString().Substring(0, 5);
            await _passportDbContext.SysCaptcha.AddAsync(new SysCaptcha()
            {
                Uuid = uuid,
                Code = code.ToString(),
                ExpireTime = DateTime.Now.AddMinutes(5)
            });
            await _passportDbContext.SaveChangesAsync();
            return createCaptcha(code.ToArray());
        }

        public async Task<bool> ValidateAsync(string uuid, string code)
        {
            SysCaptcha captchaEntity = await _passportDbContext.SysCaptcha.FindAsync(uuid);
            if (captchaEntity == null)
            {
                return false;
            }
            _passportDbContext.SysCaptcha.Remove(captchaEntity);
            await _passportDbContext.SaveChangesAsync();

            if (captchaEntity.Code.Equals(code, StringComparison.OrdinalIgnoreCase) && captchaEntity.ExpireTime >= DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
