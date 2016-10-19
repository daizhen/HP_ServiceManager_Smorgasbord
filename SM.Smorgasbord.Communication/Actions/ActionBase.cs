using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.Utils;
using SM.Smorgasbord.Communication.SoapEntities;

namespace SM.Smorgasbord.Communication.Actions
{
    public class ActionBase
    {
        #region Fields

        private int threadId = 0;

        public int ThreadId
        {
            get
            {
                return threadId;
            }
            set
            {
                threadId = value;
            }
        }

        private string soapAction;
        private DataBus dataBus;
        private bool isGzip = false;
        private bool isFastinfoset = false;

        private HttpResponseMessage responseMessage;
        private SoapBase requestData;
        private SoapBase responseData;

        #endregion 

        public bool IsGzip
        {
            get
            {
                return isGzip;
            }
            set
            {
                isGzip = value;
            }
        }

        public bool IsFastinfoset
        {
            get
            {
                return isFastinfoset;
            }
            set
            {
                isFastinfoset = value;
            }
        }

        public ActionBase(string action, DataBus bus)
        {
            this.soapAction = action;
            this.dataBus = bus;
            ThreadId = bus.ThreadId;
        }

        public string SoapAction
        {
            get
            {
                return soapAction;
            }
            set
            {
                soapAction = value;
            }
        }

        public virtual byte[] BuildMessage()
        {
            return null;
        }

        public virtual Dictionary<string, string> BuildHeaders()
        {
            return null;
        }

        public HttpResponseMessage DoAction()
        {
            dataBus.SetCommonHeaders();

            //Set additional headers
            Dictionary<string, string> additionalHeaders = BuildHeaders();
            foreach (KeyValuePair<string, string> keyValuePair in additionalHeaders)
            {
                dataBus.HttpRequest.CommonHeaders[keyValuePair.Key] = keyValuePair.Value;
            }
            dataBus.HttpRequest.CommonHeaders["SOAPAction"] = "\"" + soapAction + "\"";
            byte[] message = BuildMessage();
            byte[] convertedData = ConvertRequestMessage(message);
            dataBus.Send(convertedData);
            
            //Store it for latter use.
            responseMessage = dataBus.Receive();

            //Dispose the stream 
            //dataBus.HttpRequest.Dispose();

            return responseMessage;
        }

        private byte[] ConvertRequestMessage(byte[] rawData)
        {
            byte[] fastinfosetData = null;
            byte[] compressedData = null;

            //Fast Info Set
            if (isFastinfoset)
            {
                string message = Encoding.UTF8.GetString(rawData);
                fastinfosetData = FastinfosetUtil.GenerateFastInfoSet(message);
            }
            else
            {
                fastinfosetData = rawData;
            }

            //Gzip Compress
            if (isGzip)
            {
                compressedData = GzipUtil.Compress(fastinfosetData);
            }
            else
            {
                compressedData = fastinfosetData;
            }

            return compressedData;
        }

        public HttpResponseMessage ResponseMessage
        {
            get
            {
                return responseMessage;
            }
        }

        public virtual SoapBase ResponseData
        {
            get
            {
                return null;
            }
        }
    }
}
