using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OnlineStore.Core.Common.Contracts;

namespace OnlineStore.Core.Common.Logging
{
    public class Logger:ILogger
    {
        private readonly log4net.ILog _log;

        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            _log = log4net.LogManager.GetLogger(GetType().FullName);
        }

        public void Info(string message, Dictionary<string, string> additionalColums = null)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(message)); //message değerinin null veya boş olmadığından emin ol, yoksa exception fırlat

            BindAdditionalColumsIfNotEmpty(additionalColums);

            _log.Info(message);
        }

        public void Error(string message, Exception exception, Dictionary<string, string> additionalColums = null)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(message)); //message değerinin null veya boş olmadığından emin ol, yoksa exception fırlat
            System.Diagnostics.Contracts.Contract.Assert(exception != null); //exception değerinin null veya boş olmadığından emin ol, yoksa exception fırlat

            BindAdditionalColumsIfNotEmpty(additionalColums);

            _log.Error(message, exception);
        }

        private void BindAdditionalColumsIfNotEmpty(Dictionary<string, string> additionalColums)
        {
            if (additionalColums != null && additionalColums.Any())
            {
                foreach (var column in additionalColums)
                {
                    log4net.ThreadContext.Properties[column.Key] = column.Value;
                }
            }
        }
    }
}
