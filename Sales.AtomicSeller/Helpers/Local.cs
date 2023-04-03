using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.IO;
//using AtomicSeller.Models.DataAccessManagers;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sales.AtomicSeller.Business.Services;
using Sales.AtomicSeller.Business.IServices;

namespace Sales.AtomicSeller.Helpers
{
    public class Local
    {
        private readonly ICartService langService;

        public class InternationalMessage3
        {
            public string Token { get; set; }
            public string en_US { get; set; }
            public string fr_FR { get; set; }
            public string de_DE { get; set; }
            public string es_ES { get; set; }
            public string it_IT { get; set; }
            public string zh_CHS { get; set; }
            public string nl_NL { get; set; }
            public string zh_TW { get; set; }
            public string el_GR { get; set; }
            public string ja_JP { get; set; }
            public string pt_PT { get; set; }
            public string ru_RU { get; set; }
            public string hi_IN { get; set; }
            public string pl_PL { get; set; }
            public string id_ID { get; set; }
            public string ar_EG { get; set; }
            public string he_IL { get; set; }

            public InternationalMessage3(string _Token,
                string _en_US,
                string _fr_FR,
                string _de_DE,
                string _es_ES ,
                string _it_IT ,
                string _zh_CHS ,
                string _nl_NL ,
                string _zh_TW ,
                string _el_GR ,
                string _ja_JP ,
                string _pt_PT ,
                string _ru_RU,
                string _hi_IN,
                string _pl_PL,
                string _id_ID,
                string _ar_EG,
                string _he_IL
                )
            {
                Token = _Token;
                en_US = _en_US;
                fr_FR = _fr_FR;
                de_DE = _de_DE;
                es_ES = _es_ES;
                it_IT = _it_IT;
                zh_CHS = _zh_CHS;
                nl_NL = _nl_NL;
                zh_TW = _zh_TW;
                el_GR = _el_GR;
                ja_JP = _ja_JP;
                pt_PT = _pt_PT;
                ru_RU = _ru_RU;
                hi_IN = _hi_IN;
                pl_PL = _pl_PL;
                id_ID = _id_ID;
                ar_EG = _ar_EG;
                he_IL = _he_IL;
            }
        }

        //public static Dictionary<string, Local.InternationalMessage3> D_Lang = new Dictionary<string, Local.InternationalMessage3>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, Local.InternationalMessage3> D_Lang = new Dictionary<string, Local.InternationalMessage3>(StringComparer.OrdinalIgnoreCase);

        public static List<string> AtomicLang = new List<string>(new string[] { 
        "en-US", "fr-FR", "de-DE", "es-ES", "it-IT", "zh-CHS", "zh-CN", "nl-NL", 
        "zh-TW", "el-GR", "ja-JP", "pt-PT", "ru-RU", "hi-IN", "pl-PL", 
        "id-ID", "ar-EG", "he-IL"});

        public string TranslatedMessage(string MessageCode, string CultureCode = null, bool TokenIfEmpty = true)
        {

            if (string.IsNullOrEmpty(MessageCode)) return "";

            /*                          
            if (MyHttpContext.Current.Request.Cookies["_culture"] != null)
            {
                CultureCode = MyHttpContext.Current.Request.Cookies["_culture"];
            }
            else
            */
            {
                if (string.IsNullOrEmpty(CultureCode))
                {
                    CultureInfo Culture;
                    Culture = Thread.CurrentThread.CurrentCulture;
                    CultureCode = Culture.Name;
                }
                else
            if (CultureCode.Length == 2)
                {
                    foreach (string Lng in AtomicLang)
                        if (Lng.StartsWith(CultureCode))
                        {
                            CultureCode = Lng;
                            break;
                        }
                }
                if (AtomicLang.Contains(CultureCode) == false)
                    CultureCode = "en-US";
            }


            string Message = GetLocalizedMessage3(MessageCode, CultureCode, TokenIfEmpty);

            //Message = Message;

            return Message;
        }


        public static string StaticTranslatedMessage(string MessageCode, string CultureCode = null, bool TokenIfEmpty = true)
        {
            string _Ret = "";
            _Ret = new Local().TranslatedMessage(MessageCode, CultureCode, TokenIfEmpty);
            return _Ret;            
        }

        /* Ajout bschuller : récupération dynamique de la culture + chargement si nécessaire des données de langues */
        public string GetLocalizedMessage3(string Token, string CultureCode = null, bool TokenIfEmpty = true)
        {
            if (CultureCode == null)
            {
                CultureInfo Culture;
                Culture = Thread.CurrentThread.CurrentUICulture;
                CultureCode = Culture.Name;
            }


            string ReturnMessage = "";

            InternationalMessage3 result = null;

            try
            {
                if (!D_Lang.Any())
                    try
                    {
                        //var cart = await langService.GetCurrent(false);
                        //new DA_REF().DLang_Load();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException!=null)
                            return ex.Message + " " + ex.InnerException.Message;
                        else
                            return ex.Message;
                    }

                result = D_Lang[Token];

                //CultureCode = CultureCode;
                switch (CultureCode)
                {
                    case "en-US":
                        ReturnMessage = result.en_US;
                        break;
                    case "fr-FR":
                        ReturnMessage = result.fr_FR;
                        break;
                    case "de-DE":
                        ReturnMessage = result.de_DE;
                        break;
                    case "es-ES":
                        ReturnMessage = result.es_ES;
                        break;
                    case "it-IT":
                        ReturnMessage = result.it_IT;
                        break;
                    case "zh-CHS":
                    case "zh-CN":
                        ReturnMessage = result.zh_CHS;
                        break;
                    case "nl-NL":
                        ReturnMessage = result.nl_NL;
                        break;
                    case "zh-TW":
                        ReturnMessage = result.zh_TW;
                        break;
                    case "el-GR":
                        ReturnMessage = result.el_GR;
                        break;
                    case "ja-JP":
                        ReturnMessage = result.ja_JP;
                        break;
                    case "pt-PT":
                        ReturnMessage = result.pt_PT;
                        break;
                    case "ru-RU":
                        ReturnMessage = result.ru_RU;
                        break;
                    case "hi-IN":
                        ReturnMessage = result.hi_IN;
                        break;
                    case "pl-PL":
                        ReturnMessage = result.pl_PL;
                        break;
                    case "id-ID":
                        ReturnMessage = result.id_ID;
                        break;
                    case "ar-EG":
                        ReturnMessage = result.ar_EG;
                        break;
                    case "he-IL":
                        ReturnMessage = result.he_IL;
                        break;
                    default:
                        ReturnMessage = result.en_US;
                        break;
                }

                ReturnMessage = ReturnMessage == null ? "" : ReturnMessage;

                if (string.IsNullOrEmpty(ReturnMessage.Trim()))
                {
                    if (TokenIfEmpty == true)
                        ReturnMessage = Token;
                    else
                        ReturnMessage = " ";
                }

                return ReturnMessage;
            }
            catch (Exception)
            {
                ReturnMessage = Token;
                //new DA_REF().InsertLangToken(Token);
                return ReturnMessage;
            }
            //return ReturnMessage;
        }

    }

}
