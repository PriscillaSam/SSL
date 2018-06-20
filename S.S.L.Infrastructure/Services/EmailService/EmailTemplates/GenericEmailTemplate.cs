using S.S.L.Domain.Common;
using System.IO;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Services.EmailService.EmailTemplates
{

    public abstract class GenericEmailTemplate
    {
        public abstract Task GenerateEmailAsync();
        public string this[string file]
        {

            get
            {
                var filePath = ConfigSettings.Instance.Domain.EmailPath;

                return File.ReadAllText(Path.Combine(filePath, $"{file}.txt"));


            }
        }

    }
}
