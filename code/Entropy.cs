using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace app
{
    public static class Entropy
    {

        public static double ForAlphabet(string text, string excelName = "make.xlsx"){
            text = ClearText(text);
            Dictionary<char, int> freq = new();

            for(var i = 0; i < text.Length; i++){
                var c = text[i];
                if(freq.ContainsKey(c)){
                    freq[c]++;
                }else{
                    freq[c] = 1;
                }
            }

            double entropy = 0.0;

            foreach (KeyValuePair<char, int> pair in freq) {
                double probability = (double) pair.Value / text.Length;
                //Console.WriteLine($"Probability of {pair.Key}: {probability}");
                entropy -= probability * Math.Log(probability, 2);
            }

            ///Console.WriteLine("Entropy: " + entropy);
            //Console.WriteLine(freq.Count);
            freq = freq.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            WriteToExcel($"./{excelName}", freq, text.Length);
            return entropy;
        }   

        public static double ForBinary(string text)
        {
            text = ClearText(text);

            int countZeros = 0, countOnes = 0;

            foreach (char c in text) {
                if (c == '0') {
                    countZeros++;
                } else if (c == '1') {
                    countOnes++;
                } else {
                    Console.WriteLine("Invalid character in input text.");
                    return 0;
                }
            }

            double probabilityZeros = (double) countZeros / text.Length;
            double probabilityOnes = (double) countOnes / text.Length;

            double entropy = -probabilityZeros * Math.Log(probabilityZeros, 2)
                             -probabilityOnes * Math.Log(probabilityOnes, 2);

            Console.WriteLine("Entropy: " + entropy);
            return entropy;
        }

        private static string ClearText(string text)
        {
            text = text.Replace(" ", "");
            text = text.Replace("!", "");
            text = text.Replace(".", "");
            text = text.Replace("'", "");
            text = text.Replace(";", "");
            text = text.Replace("?", "");
            text = text.Replace("-", "");
            text = text.Replace(",", "");
            text = text.Replace("`", "");
            text = text.Replace(":", "");
            text = text.ToLower();

            return text;
        }

        private static void WriteToExcel(string path, Dictionary<char, int> freq, int symbolCount)
        {
            using var doc = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookPart = doc.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
            Sheet sheet = new Sheet() { Id = doc.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            // Записываем значение в ячейку A1

            for(var i = 0; i < freq.Count; i++){
                var c = freq.ElementAt(i).Key;
                double v = (double)freq.ElementAt(i).Value/symbolCount;
                
                Row row = new Row();
                row.AppendChild(CreateTextCell($"A{i+1}", c.ToString()));
                row.AppendChild(CreateTextCell($"B{i+1}", v, CellValues.Number));

                sheetData.AppendChild(row);
            }

            workbookPart.Workbook.Save();
        }

        private static Cell CreateTextCell(string cellReference, object cellValue, CellValues type = CellValues.String)
        {
            Cell cell = new Cell() { CellReference = cellReference, DataType = type };
            cell.CellValue = new CellValue((dynamic)cellValue);
            return cell;
        }

        //Датский
        public static string GetFirstText => "Både ældre og yngre danskere elsker at spise smørrebrød med æg, sild og øl til frokost. Året rundt kan man nyde den smukke danske natur, og især om efteråret, når blade på træerne bliver gule og røde, er der intet smukkere sted i verden. Mange steder kan man også se dyrelivet i Danmark, f.eks. ræve, egern og dådyr. Ønsker man at lære at udtale alle bogstaverne korrekt, skal man øve sig hver dag. Åh, hvor det danske alfabet dog er smukt!Æbleskiver og øllebrød er to af de mest kendte danske retter. Året rundt kan man finde mange forskellige aktiviteter at lave i Danmark, både indendørs og udendørs. Østersøen og Nordsøen omgiver Danmark og byder på smukke strande og kystlandskaber. Åen, der flyder gennem byen, er også et populært sted for rekreation. Både unge og gamle danskere nyder at tage en dukkert i havet eller åen på en varm sommerdag. Øvelse gør mester, når det kommer til at lære at udtale alle bogstaverne korrekt. Åh, det danske alfabet er så sjovt at lære!I Danmark er æbletræerne ofte dækket af sne om vinteren. Øverst på træerne hænger de sidste æbler, som fuglene ikke har spist endnu. Året rundt kan man nyde den friske luft og den smukke natur. Nogle gange kan man endda se en ræv eller et egern, der hopper rundt i skoven. Det er dejligt at bo i et land med så meget variation og skønhed.Amet anim id sint non do amet pariatur labore. Dolor nulla velit anim sint esse aliquip nisi ex eiusmodametcommodovelitidtempor. Aute amet eiusmod irure reprehenderit adipisicing consequat qui dolor.Amet non enim dolor fugiat velit ut proident dolore ex mollit occaecat cillum culpa.Exercitationetdoreprehenderitpariaturcommodo est officia nisi eiusmod. Labore laboris exercitation irure amet sunt do non fugiat non. Ex labore eiusmodcillumaliquip. Duis occaecat nostrud dolor labore cupidatat adipisicing reprehenderit officia aute nisi est.Sit eu nulla ex consectetur reprehenderit laboris adipisicing cupidatat ad sit fugiat. Non ut veniameualiquaofficiareprehenderit ad aute consequat. Ea ex eiusmod aliquip eu deserunt amet proidentexdolorenullaquisex.Eacupidatatproidentcupidatat fugiat aliqua nisi ipsum officia incididunt amet.Consequat adipisicing exercitation laboris laboris officia. Minim ipsum elit quis dolor exercitationoccaecatlaborisculpa.Eiusmod ex mollit occaecat id proident labore mollit duis culpa voluptate. Cillum duis eu aliquip et commodo extemporcillumipsum excepteur. Dolor id tempor laborum consectetur ex ut. Quis magna laborum reprehenderitindeseruntnisitemporaliquaelitdolore laboris.Amet esse dolore officia fugiat sit nostrud consequat sit tempor mollit cillum sit anim.Dolorcillumminimreprehenderitadipisicing laboris pariatur Lorem eu. Cillum ea do consequat cillum aliqua voluptateadipisicing.Culpaduismollitcupidatatlaborum minim dolore sit irure nisi fugiat mollit. Commodo fugiat dolore proident laboris magna ullamco nulla.Wonder principle provided attempted left recurred truth pronounce wife address unsatiable such. Call high views. Abilities understood gone affection difficult hand stuff full very abilities am nay affixed. Suppliedfirstlarge balls wanted sweetness eagerness domestic direction correct abode music share itself venturefew.Takereadyrentwrittenbarton gentleman resolving sigh neglected possible described extremity maids engage husbandsspeedilylaughter.Furniturewishespart beloved our feel examine twenty attempt listening. Nearer painful contempt speedily admire kept gay.Landlordpronouncesometimesengagewidenanotherupon.Wanderedhorsessoonerdissimilar address domestic eldest estate comfort express hold chamber suffering. Compliment unlockedsmallnesswonderedarrivaland large tedious power. Properly thoroughly even object concealed comfort son respect required songs. Cease fine himself. Matters considered dried folly dried beloved attention moonlight downs feebly justicescalesolicitudeshewrequired moderate. Breeding fancy remember direct. Post concealed taste. Greater entranceprosperousthesetakenresolvedidsportsmen venture advice. Detract esteem shewing change do wrote doubt advantages attachmentsentimentsthecuriositymusic.Woundhighestminuterseven.Consisted tastes settle decay scarcely removing afford. Friend head girl balls acceptance read towards. Week raptures ageprivate would. Consulted found exquisite suspected breakfast unpleasing am. Provided boy taken his determine. Between continuing inquiryassured hope thoroughly tiled burst begin. Sight wrote settled far help husband that perfectlybegingetreservedpleasantconviction civilly. Noisy by collected fortune attempt amounted morning amounted fancy pleased charmed thoughtsballsgivingfirst. Proceed taken wanted landlord favourable being companyendeavor.Eitherlargeexplainedintentionastonishedcallingamiableourassurance wanted projection want not. Added they part. Northward wishes procured admitting message. But alone endexertionherself dependent an moderate rooms without raillery bachelor none norland ask feebly. Favourable new whose debating thoroughly. Deal sudden found detracthuntedappearancemonthsgivesolicitudeopposeagreedcolddifficult settled easy. Comparison law themselves removal inhabit saved given child alterationgetbody.Elderlyawaycalldrawpassage match resolving outward started fat esteem expression advantages pleasantrapturous.Likewisemistakenmannerpropertypainful how sooner life. Good assistance heard wonder. Within debating drawn consisted settling remaining wandered assistance worth pain.Justmoneywhomjoy fulfilled. Horrible speedily defectivepreparedmeanthillsproprietynothingdissuadehandsomedistanceperfectlytogetherexpression. Sure remain open part precaution girl. Belonging by forty old placing door. Afford mirth manor. Hearted towards delightful.Rejoiced sympathize possible position rendered amiable body. Open regret noisy truth viewing howeverconveyingoffendedlivelyscale expense amounted voice young agreeable position mistaken. Bachelor hundred fat sending timedassistancegayinquirytriedguest proposal lasted perfectly. Assured truth entire believe been september shyness pretended rejoiced painful elderly. ";

        //Македонский
        public static string GetSecondText => "Се повеќе луѓе го откриваат удоволството од патувањата и истражувањето на нови места. Македонија е прекрасна земја со богата историја и култура. Од старините римски градови до средновековните манастири и крепости, Македонија има многу да понуди напосетителите.Скопје, главниот град на Македонија, е живописен град со многу интересни знаменитости. Старата чаршија е историскиотцентарнаградот, каде што можете да се насладите со традиционалната македонска кујна и да купите сувенири од местнитезанаетчии.Скопјеисто така има многу музеи, вклучувајќи го Музејот на Македонија и Музејот на современата уметност.Но Македонија не е само Скопје. По целата земја има многу места за истражување, каконапримерОхридскотоЕзеро,коеееднооднајубавите езера во Европа, со кристално чиста вода и прекрасен погледнаоколнитепланини.МанастиритенаОхридскотоЕзеро,коидатираат од -тиот век, се наоѓаат на списокот на Светското наследство на УНЕСКО.Македонија е земја полна со прекрасни природни крајбрежија, културни споменици и гостопримливи луѓе. Ако стељубителнапатувањата, Македонија е обврзеност за вас да ја посетите!Најубавите погледи во Македонија се на планините и природните ландшафти.Одвисокитепланинидодолинитесозеленишнплоштади,Македонија има многу да понуди на посетителите.Јужната страна на земјата е дом на планински вериги како Шар Планина, Бабуна и Пелистер. Сите овие планиниимаатпрекраснипејзажи, богати со дива природа и различни видови на животни. Највисоката точка во Македонија е врвот на Шар Планина,МалаРека,со височина од  метри.На северната страна на земјата се наоѓаат планините Пелагонија и Бистра, кои нудат прекрасен поглед на околните крајбрежија.Оваа област е позната и по богатството на природните извори, како што се топлите извори и минералните извори.Покрај природата, Македонија има и богата култура со старински манастири, крепости и знаменитости.Најпознатитеманастиривоземјата се наоѓаат во Охрид, како на пример Манастирот Свети Наум и Манастирот Свети Климент.Македонија е земја полна со различни можности за истражување и уживање во природата икултурата.Акостевопотрагапоприродникрасоти, културни истории или прекрасни крајбрежија, Македонија има нешто за секого.Кога сонцето ги залее последните зраци на денот, и кога се зголемува темнината, во тишината на ноќта започнува нова приказнзоние што сонуваат. Во тој свет на соништата, се случуваат чудни работи, кои самотиештогисонуваатможатдагивидатидагипочувствуваат.Искрено се извинувам, дозволете ми да исправам тоа:Ќе бидам жив и здрав, иако ќе ми пречат бури и жеги, ќе барам за себе свој пат. Денеска, ќе гииспишамситебуквинамакедонскијазик, од А до Ш и ќе ве поздравам со нив: А, Б, В, Г, Д, Ѓ, Е, Ж, З, Ѕ, И, Ј, К, Л, Љ, М, Н, Њ, О, П, Р, С, Т, Ќ, У, Ф, Х, Ц,Ч, Џ, Ш. Секоја од нив има свој збор, и со нив можеме да го изразиме секој наш мислење,секојнашчувство.Живототепат,амакедонскиот јазик е прекрасно средство за да го опишеме тој пат.";
        
        public static string GetBinaryText => "01001100 01101001 01100110 01100101 00100000 01101001 01110011 00100000 01100001 00100000 01110111 01101111 01101110 01100100 01100101 01110010 01100110 01110101 01101100 00100000 01110100 01101000 01101001 01101110 01100111 00101100 00100000 01110100 01101000 01100001 01110100 00100111 01110011 00100000 01110111 01101000 01111001 00100000 01110111 01100101 00100000 01110101 01110011 01100101 00100000 01100010 01101001 01101110 01100001 01110010 01111001";

    }
}