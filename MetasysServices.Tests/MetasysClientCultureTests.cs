using System;
using NUnit.Framework;
using JohnsonControls.Metasys.BasicServices;
using System.Globalization;
using System.Diagnostics;
using JohnsonControls.Metasys.BasicServices.Utils;

namespace MetasysServices.Tests
{
    public class MetasysClientCultureTests
    {
        private const string Reliable = "reliabilityEnumSet.reliable";
        private const string PriorityNone = "writePriorityEnumSet.priorityNone";
        private const string Unsupported = "statusEnumSet.unsupportedObjectType";
        private const string Array = "dataTypeEnumSet.arrayDataType";
        MetasysClient client;

        [OneTimeSetUp] 
        public void Init()
        {
            client = new MetasysClient("hostname");
        }

        [TestCase("Reliable", "Unsupported object type", "0 (No Priority)", "Array")]
        public void TestLocalizeCultureUnsupportedCulture(string reliable, string unsupported, string priority, string array)
        {
            CultureInfo culture = new CultureInfo("fr-LU");
            Assert.AreEqual(reliable, ResourceManager.Localize(Reliable, culture));
            Assert.AreEqual(unsupported, ResourceManager.Localize(Unsupported, culture));
            Assert.AreEqual(priority, ResourceManager.Localize(PriorityNone, culture));
            Assert.AreEqual(array, ResourceManager.Localize(Array, culture));
        }

        [TestCase("Reliable", "Unsupported object type", "0 (No Priority)", "Array")]
        public void TestLocalizeCultureDefault(string reliable, string unsupported, string priority, string array)
        {
            Assert.AreEqual(reliable, client.Localize(Reliable));
            Assert.AreEqual(unsupported, client.Localize(Unsupported));
            Assert.AreEqual(priority, client.Localize(PriorityNone));
            Assert.AreEqual(array, client.Localize(Array));

            Assert.AreEqual(reliable, ResourceManager.Localize(Reliable));
            Assert.AreEqual(unsupported, ResourceManager.Localize(Unsupported));
            Assert.AreEqual(priority, ResourceManager.Localize(PriorityNone));
            Assert.AreEqual(array, ResourceManager.Localize(Array));
        }

        [TestCase("en-US", "Reliable", "Unsupported object type", "0 (No Priority)", "Array")]
        [TestCase("cs-CZ", "V??rohodn??", "Nepodporovan?? typ objektu", "0 (Bez priority)", "Pole")]
        [TestCase("de-DE", "Zuverl??ssig", "Nicht-unterst??tzter Objekttyp", "0 (Keine Priorit??t)", "Anordnung")]
        [TestCase("es-ES", "Fiable", "Tipo de objeto no admitido", "0 (Sin prioridad)", "Matriz")]
        [TestCase("fr-FR", "Fiable", "Type d'objet non pris en charge", "0 (aucune priorit??)", "Tableau")]
        [TestCase("hu-HU", "Megb??zhat??", "Nem t??mogatott objektumt??pus", "0 (Nincs priorit??s)", "T??mb")]
        [TestCase("it-IT", "Affidabile", "Tipo di oggetto non supportato", "0 (Nessuna Priorit??)", "Vettore")]
        [TestCase("ja-JP", "??????????????????", "????????????????????????????????? ?????????", "0(??????????????????)", "?????????")]
        [TestCase("ko-KR", "??????", "???????????? ?????? ???????????? ??????", "0 (???????????? ??????)", "??????")]
        [TestCase("nb-NO", "P??litelig", "Objekttype som ikke st??ttes", "0 (Ingen prioritet)", "Tabell")]
        [TestCase("nl-NL", "Betrouwbaar", "Niet-ondersteund objecttype", "0 (geen prioriteit)", "Matrix")]
        [TestCase("pl-PL", "Niezawodny", "Nieobs??ugiwany typ obiektu", "0 (Brak priorytetu)", "Tablica")]
        [TestCase("pt-BR", "Confi??vel", "Tipo de objeto n??o suportado", "0 (Sem Prioridade)", "Matriz")]
        [TestCase("ru-RU", "????????????????", "???????????????????????????????? ?????? ????????????????", "0 (?????????????????? ??????????????????????)", "????????????")]
        [TestCase("sv-SE", "Tillf??rlitlig", "Objekttypen st??ds inte", "0 (Ingen prioritet)", "Upps??ttning")]
        [TestCase("tr-TR", "G??venilir", "Desteklenmeyen nesne t??r??", "0 (??ncelik Yok)", "Dizi")]
        [TestCase("zh-Hans-CN", "??????", "????????????????????????", "0 (????????????)", "??????")]
        [TestCase("zh-Hant-TW", "??????", "????????????????????????", "0 (????????????)", "??????")]
        public void TestLocalizeCulture(string culture, string reliable, string unsupported, string priority, string array)
        {
            CultureInfo testCulture = new CultureInfo(culture);
            Assert.AreEqual(reliable, client.Localize(Reliable, testCulture));
            Assert.AreEqual(unsupported, client.Localize(Unsupported, testCulture));
            Assert.AreEqual(priority, client.Localize(PriorityNone, testCulture));
            Assert.AreEqual(array, client.Localize(Array, testCulture));
        }

        public void TestLocalizeDictionaries()
        {
            Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            string commandEnum = ResourceManager.GetCommandEnumeration("Reset Field Device");
            timer.Stop();
            Assert.AreEqual("commandIdEnumSet.resetFieldDeviceCommand", commandEnum);
            Assert.GreaterOrEqual(2, timer.Elapsed.TotalSeconds);

            timer.Reset();
            timer.Start();
            string typeEnum = ResourceManager.GetObjectTypeEnumeration("AV");
            timer.Stop();
            Assert.AreEqual("objectTypeEnumSet.bacAvClass", commandEnum);
            Assert.GreaterOrEqual(2, timer.Elapsed.TotalSeconds);
        }
    }
}
