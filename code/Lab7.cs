using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace app
{
    public class Lab7
    {
        public static void Main() 
        {
            //var plainText = "Hello worlds";
            var plainText = "Compliment speedily remaining around exposed favour forty valley request joy drew blessing possession sisterwished missed. Stimulated interest under continued still praise down continual folly. Suppliedstylesoonercuriosity sussex vicinity concluded any friendship. Having part put returned sending as dissuadegoodfrequentlythem noisier. Moderate preference settling praise consider belonging any betrayed stronger detract knewlacertainty judgment. ";
            plainText = "Improved hold related bringing did. Luckily child education. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design ladies object has however though reserved. Years busy uncommonly outlivedquickquiethalf size reserved though same means found offices oppose besides often. Rose mirth that produced girl better. Chapter felt ten betrayed defective. Concerns chiefly appetite exercise play theirsevilstillneedthesesendingtemper ten looked least covered. Affronting zealously behaved collecting behaved passage order most had hold.Fancy front middletons company answered told giving civilly excuse society polite elsewhere mirth beyond dealfond. View welcomed arise many. Seems linen compass welcomed yet agreeable followed miss stairs collected lived tell humoured conduct comparison. Joy acuteness interested thought come furnished perhaps spot delivered drew. Enjoyment interested over. Dried discovery hastily exquisite point answer just. Smart put mother point relied window readdecayfewventurehammeproceed tell thoughts discovery that. However cheered gay effect world raptures therefore manners excited. Boy least hopewoodedtendedlainsuspectedinnate vanity greatest juvenile west forfeited roused finished pasture. Talked offershotmeantgoingoughtyearscoming chicken dare upon effects concluded society settled ample yourself. Had one followed ability beingdirection unpleasant income unknown well hardly tiled eat neat blush however matters.Wisebrotherthoroughlyfinished property bred exquisite then morning secure opinion. Cause not taken gave inhabiting advanced. Assured melancholy amounted however cousin linen vexed given promotion why nay sorry conveying determine. Basket misery continue satisfied stairs three. Suitable the death walk assurance months cheerful wooded pleasure could journey repulsive marianne behind depart appearance. Observe gay delivered affronting hope pleasant cease resolved cause waited cannot amongst examine leave. Matter on better principles many thrown depend. Alteration assured been law that three preferred compact children event additions rapid sympathize exeter. Lively domestic passed subjects settling moonlight handsome engrossed when beauty prepared daughters. Maids civil other perceive attending added abroad edward did downs burstraising.Its proposal edward difficult her smallness. Declared size led delight behaved formal talked. Matter greatest forbade mistaken ought felt pursuit savingssecure being held narrow offence. Gravity lasting sang securing. New merry agreed sitting september acutenesseach. Colonel picture little parish above within new sooner found marry fond remark therefore itselfvisitdiverted. Denied rapturous evil time natural law. Enjoy left good principle entrancehungmiddletoncultivated.Agreedfavourable dried resembled arrived kindness pasture dependingyoustimulatedinstrumentrecurred.Faceadditionimproving being match besides wish studied enabled newspaper savings satisfied it. Danger amounteddejectionoffence come resolved order another favourite appearance parlors seeing delicate. Lady ourselves jointure pulled afford judgment landlord followed sir beauty almost pretty compass. Advantage mistaken placing convinced terms misery estimating thirty which sigh maids abroad introduced cause might along. Vulgar called change humoured for reasonable alone offended carried. Praise affixed additions goodness wishes wound spring answer outweigh. Coming man points high readzealouslsportsmanexquisitewittyoccasionstyleshallhousehold. Sure truth favourable spoil my handsome enable general now moonlight linen woody branch confined. Brought venture forfeited am next drawn smile rest dare extent assure surprise game for. Oh building astonished imprudence settling theirs name listening demesne exquisite. Collected merry whole depend over heard genius literature left weddings much. Man half limits advanced son eldest stairs graceful request saved esteem him. Parish fine style humanity sitting want. Against enough consulted. Often while winding viewonlythinklikewisemother right village sometimes will being expenses. Vicinity square blind walk frequentlyaroserenderedrentgoodness his sympathize pain private roof favourable alteration returned. Effectsexertionmanorpursetimesotherwise. Neat has added head theirs body resolve allow. Apartments daughter after them blessing surroundeddwellinghappiness day estate supplied done hastily situation. Meant through instantly unable much rich girlminuterunlocked spot. Are perpetual received. Before depend settled said. Believed valley welcome uncommonly own. Figure insensible procuring temper marked proposal objectroomsquicksociety goodness bed. Of relied forfeited towards sitting dining enjoy concerns raptures whenformedup.Amongelderly wife off pulled come offence position particular mean regular six goingesteemdelightfullose.Pictureashamed need wicket fortune disposing gay bed depend horses merry attacks burst conveying. Repeated distant breakfast off last state. Miles you shortly wooded branch cousin september formerly comparison goodness just neat hearted simplicity. Procuring vicinity wished servants found merry brother chapter correct change be latter attempted county. Men small savings comfort alone butabovedonesociableherselffar.Sheddidentreaties cordially weeks calling. Being expect enjoyment have forth dinner rejoicedthoughtsakeintentionpolitecome.Severaldiscoveredmistress.Acceptance show went tried shew turned securing might meritobjectionpossibleexplainedlettersleastchamberextremely. Occasional excellent above right make compass rich easily entire ourselves would ferrars.Talkedraillery mistress course boisterous from steepest hold certainly unpleasing cordially which new. Themselves greater arrived most case depart affixed feel each speaking face beauty dejection. Going pretended sociable rank made ham parties fanny name arise lady dejection son him should. Material hard branch snug repair landlord greater acuteness moments thrown alone indeed about families party. Except resembled above sentadmiregenius hastily gone heard. Speaking are dull wisdom enabled discourse prospect wish resolved anprojectingsportsmen quick remarkably no. Edward juvenile smallness pleasure. Are preventnotmariannedayunpleasanthopesnatureenquirehighlymarriageinvitation. Household tears elinor match. Our certainty preferencebeginsuspicionammayconvictionladyweddingsexpense occasion our. Noise blush miles other engaged enjoy basketunsatiablemaydecaypossiblecertaintypasturedirect see. Feel speedily know others consisted behaved yet demands.Noisydifferededwardseeingstartedlikewisebelievingdependent met feelings indulgence whatever equally the. Remain true removed into. ";
            Console.WriteLine($"Plain text length: {plainText.Length}");
            var key = "senchen";

            var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            // var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
            var keyBytes = GenerateDesKey(key, 1);
            var ivBytes = new byte[8];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var encryptedBytes = EncryptDES(plainBytes, keyBytes, ivBytes);

            stopwatch.Stop();
            Console.WriteLine($"Time to encrypt: {stopwatch.ElapsedMilliseconds} ms");

            var encryptedText = Convert.ToBase64String(encryptedBytes);

            stopwatch.Start();

            var decryptedBytes = DecryptDES(encryptedBytes, keyBytes, ivBytes);

            stopwatch.Stop();
            Console.WriteLine($"Time to decrypt: {stopwatch.ElapsedMilliseconds} ms");

            var decryptedText = System.Text.Encoding.UTF8.GetString(decryptedBytes);

            Console.WriteLine(encryptedText);
            Console.WriteLine(decryptedText);

            
        }

        public static byte[] GenerateDesKey(string inputKey, int iteration)
        {
            if(inputKey.Length != 7)
                throw new ArgumentException("Invalid key size");

            using (var sha1 = SHA1.Create())
            {
                // Добавляем номер итерации к входной строке
                string input = inputKey + iteration.ToString();

                // Преобразуем входную строку в байтовый массив
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);

                // Вычисляем хэш SHA1 от входных данных
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                // Дополняем хэш нулями, если его длина меньше 8 байт
                byte[] keyBytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    if (i < hashBytes.Length)
                        keyBytes[i] = hashBytes[i];
                    else
                        keyBytes[i] = 0;
                }

                Console.WriteLine($"key: ${Convert.ToBase64String(keyBytes)} iteration: ${iteration}");

                // Возвращаем сгенерированный ключ
                return keyBytes;
            }
        }

        static byte[] EncryptDES(byte[] plainBytes, byte[] keyBytes, byte[] iv)
        {
            int blockCount = (int)Math.Ceiling(plainBytes.Length / 8.0);
            byte[] paddedBytes = new byte[blockCount * 8];
            Array.Copy(plainBytes, paddedBytes, plainBytes.Length);
            
            byte[] encryptedBytes = new byte[paddedBytes.Length];
            byte[] inputBlock = new byte[8];
            byte[] outputBlock = new byte[8];
            
            for (int i = 0, keyI = 0; i < blockCount; i++)
            {
                Array.Copy(paddedBytes, i * 8, outputBlock, 0, 8);

                Console.WriteLine($"Block: {System.Text.Encoding.UTF8.GetString(outputBlock)}");

                for(var n = 0; n < 16; n++)
                {
                    outputBlock = EncryptDESBlock(outputBlock, keyBytes, iv);
                    //Console.WriteLine($"Block after step {n+1}: {Convert.ToBase64String(outputBlock)}");
                }   
                
                Array.Copy(outputBlock, 0, encryptedBytes, i * 8, 8);
            }
        
            return encryptedBytes;
        }

        static byte[] EncryptDESBlock(byte[] inputBlock, byte[] key, byte[] iv)
        {
            byte[] outputBlock = new byte[8];
            
            DES des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.None;
            des.Key = key;
            des.IV = iv;

            des.CreateEncryptor().TransformBlock(inputBlock, 0, 8, outputBlock, 0);
            
            return outputBlock;
        }

        static byte[] DecryptDESBlock(byte[] inputBlock, byte[] key, byte[] iv)
        {
            byte[] outputBlock = new byte[8];
            
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.CBC;
            desProvider.Padding = PaddingMode.None;
            ICryptoTransform decryptor = desProvider.CreateDecryptor(key, iv);
            
            decryptor.TransformBlock(inputBlock, 0, 8, outputBlock, 0);
            
            return outputBlock;
        }

        public static byte[] DecryptDES(byte[] encryptedBytes, byte[] keyBytes, byte[] ivBytes)
        {
            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            byte[] inputBlock = new byte[8];
            byte[] outputBlock = new byte[8];
            
            Array.Copy(ivBytes, outputBlock, 8);
            
            for (int i = 0, keyI = 0; i < encryptedBytes.Length / 8; i++)
            {
                Array.Copy(encryptedBytes, i * 8, outputBlock, 0, 8);
                Console.WriteLine($"block for decryption: {Convert.ToBase64String(outputBlock)}");

                for(var n = 0; n < 16; n++)
                {
                    outputBlock = DecryptDESBlock(outputBlock, keyBytes, ivBytes);
                    //Console.WriteLine($"Decrypt block after step {n+1}: {Convert.ToBase64String(outputBlock)}");
                }
                
                Array.Copy(outputBlock, 0, decryptedBytes, i * 8, 8);
            }
            
            int paddedCount = 0;
            
            for (int i = decryptedBytes.Length - 1; i >= 0; i--)
            {
                if (decryptedBytes[i] == 0)
                {
                    paddedCount++;
                }
                else
                {
                    break;
                }
            }
            
            byte[] resultBytes = new byte[decryptedBytes.Length - paddedCount];
            Array.Copy(decryptedBytes, resultBytes, resultBytes.Length);
            return resultBytes;
        }
    }
}