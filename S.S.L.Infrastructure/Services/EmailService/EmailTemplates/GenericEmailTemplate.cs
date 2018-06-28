using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{


    public abstract class GenericEmailTemplate
    {

        public abstract Task GenerateEmailAsync();
        public string this[string file]
        {

            get
            {
                var filePath = HttpContext.Current.Server.MapPath("~/Emails/");

                return File.ReadAllText(Path.Combine(filePath, $"{file}.txt"));
            }
        }

    }
}
