using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using app.lab11;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;

namespace app.lab12
{
    public class Lab12
    {
        public static void Main() {
            Console.WriteLine("With RSA:");
            WithRsa();

            Console.WriteLine("\nWith ElGamal:");
            WithElgamal();

            Console.WriteLine("\nWith Schnorr:");
            WithSchnorr();
        }

        public static void WithRsa() 
        {
            // Создаем новый объект класса RSA
            using RSA rsa = RSA.Create();

            // Получаем ключи
            RSAParameters publicKey = rsa.ExportParameters(false);
            RSAParameters privateKey = rsa.ExportParameters(true);

            // Определяем сообщение, которое нужно подписать
            byte[] message = Encoding.UTF8.GetBytes("Hello, Vlad!");
            message = Encoding.UTF8.GetBytes("Improved hold related bringing did. Luckily child education. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design ladies object has however though reserved. Years busy uncommonly outlivedquickquiethalf size reserved though same means found offices oppose besides often. Rose mirth that produced girl better. Chapter felt ten betrayed defective. Concerns chiefly appetite exercise play theirsevilstillneedthesesendingtemper ten looked least covered. Affronting zealously behaved collecting behaved passage order most had hold.Fancy front middletons company answered told giving civilly excuse society polite elsewhere mirth beyond dealfond. View welcomed arise many. Seems linen compass welcomed yet agreeable followed miss stairs collected lived tell humoured conduct comparison. Joy acuteness interested thought come furnished perhaps spot delivered drew. Enjoyment interested over. Dried discovery hastily exquisite point answer just. Smart put mother point relied window readdecayfewventurehammeproceed tell thoughts discovery that. However cheered gay effect world raptures therefore manners excited. Boy least hopewoodedtendedlainsuspectedinnate vanity greatest juvenile west forfeited roused finished pasture. Talked offershotmeantgoingoughtyearscoming chicken dare upon effects concluded society settled ample yourself. Had one followed ability beingdirection unpleasant income unknown well hardly tiled eat neat blush however matters.Wisebrotherthoroughlyfinished property bred exquisite then morning secure opinion. Cause not taken gave inhabiting advanced. Assured melancholy amounted however cousin linen vexed given promotion why nay sorry conveying determine. Basket misery continue satisfied stairs three. Suitable the death walk assurance months cheerful wooded pleasure could journey repulsive marianne behind depart appearance. Observe gay delivered affronting hope pleasant cease resolved cause waited cannot amongst examine leave. Matter on better principles many thrown depend. Alteration assured been law that three preferred compact children event additions rapid sympathize exeter. Lively domestic passed subjects settling moonlight handsome engrossed when beauty prepared daughters. Maids civil other perceive attending added abroad edward did downs burstraising.Its proposal edward difficult her smallness. Declared size led delight behaved formal talked. Matter greatest forbade mistaken ought felt pursuit savingssecure being held narrow offence. Gravity lasting sang securing. New merry agreed sitting september acutenesseach. Colonel picture little parish above within new sooner found marry fond remark therefore itselfvisitdiverted. Denied rapturous evil time natural law. Enjoy left good principle entrancehungmiddletoncultivated.Agreedfavourable dried resembled arrived kindness pasture dependingyoustimulatedinstrumentrecurred.Faceadditionimproving being match besides wish studied enabled newspaper savings satisfied it. Danger amounteddejectionoffence come resolved order another favourite appearance parlors seeing delicate. Lady ourselves jointure pulled afford judgment landlord followed sir beauty almost pretty compass. Advantage mistaken placing convinced terms misery estimating thirty which sigh maids abroad introduced cause might along. Vulgar called change humoured for reasonable alone offended carried. Praise affixed additions goodness wishes wound spring answer outweigh. Coming man points high readzealouslsportsmanexquisitewittyoccasionstyleshallhousehold. Sure truth favourable spoil my handsome enable general now moonlight linen woody branch confined. Brought venture forfeited am next drawn smile rest dare extent assure surprise game for. Oh building astonished imprudence settling theirs name listening demesne exquisite. Collected merry whole depend over heard genius literature left weddings much. Man half limits advanced son eldest stairs graceful request saved esteem him. Parish fine style humanity sitting want. Against enough consulted. Often while winding viewonlythinklikewisemother right village sometimes will being expenses. Vicinity square blind walk frequentlyaroserenderedrentgoodness his sympathize pain private roof favourable alteration returned. Effectsexertionmanorpursetimesotherwise. Neat has added head theirs body resolve allow. Apartments daughter after them blessing surroundeddwellinghappiness day estate supplied done hastily situation. Meant through instantly unable much rich girlminuterunlocked spot. Are perpetual received. Before depend settled said. Believed valley welcome uncommonly own. Figure insensible procuring temper marked proposal objectroomsquicksociety goodness bed. Of relied forfeited towards sitting dining enjoy concerns raptures whenformedup.Amongelderly wife off pulled come offence position particular mean regular six goingesteemdelightfullose.Pictureashamed need wicket fortune disposing gay bed depend horses merry attacks burst conveying. Repeated distant breakfast off last state. Miles you shortly wooded branch cousin september formerly comparison goodness just neat hearted simplicity. Procuring vicinity wished servants found merry brother chapter correct change be latter attempted county. Men small savings comfort alone butabovedonesociableherselffar.Sheddidentreaties cordially weeks calling. Being expect enjoyment have forth dinner rejoicedthoughtsakeintentionpolitecome.Severaldiscoveredmistress.Acceptance show went tried shew turned securing might meritobjectionpossibleexplainedlettersleastchamberextremely. Occasional excellent above right make compass rich easily entire ourselves would ferrars.Talkedraillery mistress course boisterous from steepest hold certainly unpleasing cordially which new. Themselves greater arrived most case depart affixed feel each speaking face beauty dejection. Going pretended sociable rank made ham parties fanny name arise lady dejection son him should. Material hard branch snug repair landlord greater acuteness moments thrown alone indeed about families party. Except resembled above sentadmiregenius hastily gone heard. Speaking are dull wisdom enabled discourse prospect wish resolved anprojectingsportsmen quick remarkably no. Edward juvenile smallness pleasure. Are preventnotmariannedayunpleasanthopesnatureenquirehighlymarriageinvitation. Household tears elinor match. Our certainty preferencebeginsuspicionammayconvictionladyweddingsexpense occasion our. Noise blush miles other engaged enjoy basketunsatiablemaydecaypossiblecertaintypasturedirect see. Feel speedily know others consisted behaved yet demands.Noisydifferededwardseeingstartedlikewisebelievingdependent met feelings indulgence whatever equally the. Remain true removed into. ");

            // Создаем объект класса SHA256 для вычисления хеша сообщения
            using SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(message);

            // Создаем объект класса RSAPKCS1SignatureFormatter для генерации подписи
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsa);

            // Устанавливаем хеш-алгоритм для вычисления подписи
            formatter.SetHashAlgorithm("SHA256");

            // Создаем подпись
            var watch = new Stopwatch();
            watch.Start();
            byte[] signature = formatter.CreateSignature(hash);
            watch.Stop();
            Console.WriteLine($"Signature: {Lab11.BToString(signature)}");
            Console.WriteLine($"RSA signature time: {watch.ElapsedMilliseconds} ms");

            // Создаем объект класса RSAPKCS1SignatureDeformatter для верификации подписи
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                
            // Устанавливаем хеш-алгоритм для вычисления подписи
            deformatter.SetHashAlgorithm("SHA256");

            // Проверяем подпись
            watch.Start();
            bool verified = deformatter.VerifySignature(hash, signature);
            watch.Stop();
            Console.WriteLine($"RSA verify time: {watch.ElapsedMilliseconds} ms");
            
            Console.WriteLine($"Verified: {verified}");
        }

        public static void WithElgamal() 
        {
           ElGamal.Main();
        }
        

        public static void WithSchnorr() 
        {
            using (ECDsaCng schnorr = new ECDsaCng(ECCurve.NamedCurves.nistP256))
            {
                
                schnorr.HashAlgorithm = CngAlgorithm.Sha256;
                var watch = new Stopwatch();
                watch.Start();
                byte[] privateKey = schnorr.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
                byte[] publicKey = schnorr.Key.Export(CngKeyBlobFormat.EccPublicBlob);

                // Подпись данных закрытым ключом
                byte[] dataToSign = Encoding.UTF8.GetBytes("Hello, Vlad!");
                dataToSign = Encoding.UTF8.GetBytes("Improved hold related bringing did. Luckily child education. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design ladies object has however though reserved. Years busy uncommonly outlivedquickquiethalf size reserved though same means found offices oppose besides often. Rose mirth that produced girl better. Chapter felt ten betrayed defective. Concerns chiefly appetite exercise play theirsevilstillneedthesesendingtemper ten looked least covered. Affronting zealously behaved collecting behaved passage order most had hold.Fancy front middletons company answered told giving civilly excuse society polite elsewhere mirth beyond dealfond. View welcomed arise many. Seems linen compass welcomed yet agreeable followed miss stairs collected lived tell humoured conduct comparison. Joy acuteness interested thought come furnished perhaps spot delivered drew. Enjoyment interested over. Dried discovery hastily exquisite point answer just. Smart put mother point relied window readdecayfewventurehammeproceed tell thoughts discovery that. However cheered gay effect world raptures therefore manners excited. Boy least hopewoodedtendedlainsuspectedinnate vanity greatest juvenile west forfeited roused finished pasture. Talked offershotmeantgoingoughtyearscoming chicken dare upon effects concluded society settled ample yourself. Had one followed ability beingdirection unpleasant income unknown well hardly tiled eat neat blush however matters.Wisebrotherthoroughlyfinished property bred exquisite then morning secure opinion. Cause not taken gave inhabiting advanced. Assured melancholy amounted however cousin linen vexed given promotion why nay sorry conveying determine. Basket misery continue satisfied stairs three. Suitable the death walk assurance months cheerful wooded pleasure could journey repulsive marianne behind depart appearance. Observe gay delivered affronting hope pleasant cease resolved cause waited cannot amongst examine leave. Matter on better principles many thrown depend. Alteration assured been law that three preferred compact children event additions rapid sympathize exeter. Lively domestic passed subjects settling moonlight handsome engrossed when beauty prepared daughters. Maids civil other perceive attending added abroad edward did downs burstraising.Its proposal edward difficult her smallness. Declared size led delight behaved formal talked. Matter greatest forbade mistaken ought felt pursuit savingssecure being held narrow offence. Gravity lasting sang securing. New merry agreed sitting september acutenesseach. Colonel picture little parish above within new sooner found marry fond remark therefore itselfvisitdiverted. Denied rapturous evil time natural law. Enjoy left good principle entrancehungmiddletoncultivated.Agreedfavourable dried resembled arrived kindness pasture dependingyoustimulatedinstrumentrecurred.Faceadditionimproving being match besides wish studied enabled newspaper savings satisfied it. Danger amounteddejectionoffence come resolved order another favourite appearance parlors seeing delicate. Lady ourselves jointure pulled afford judgment landlord followed sir beauty almost pretty compass. Advantage mistaken placing convinced terms misery estimating thirty which sigh maids abroad introduced cause might along. Vulgar called change humoured for reasonable alone offended carried. Praise affixed additions goodness wishes wound spring answer outweigh. Coming man points high readzealouslsportsmanexquisitewittyoccasionstyleshallhousehold. Sure truth favourable spoil my handsome enable general now moonlight linen woody branch confined. Brought venture forfeited am next drawn smile rest dare extent assure surprise game for. Oh building astonished imprudence settling theirs name listening demesne exquisite. Collected merry whole depend over heard genius literature left weddings much. Man half limits advanced son eldest stairs graceful request saved esteem him. Parish fine style humanity sitting want. Against enough consulted. Often while winding viewonlythinklikewisemother right village sometimes will being expenses. Vicinity square blind walk frequentlyaroserenderedrentgoodness his sympathize pain private roof favourable alteration returned. Effectsexertionmanorpursetimesotherwise. Neat has added head theirs body resolve allow. Apartments daughter after them blessing surroundeddwellinghappiness day estate supplied done hastily situation. Meant through instantly unable much rich girlminuterunlocked spot. Are perpetual received. Before depend settled said. Believed valley welcome uncommonly own. Figure insensible procuring temper marked proposal objectroomsquicksociety goodness bed. Of relied forfeited towards sitting dining enjoy concerns raptures whenformedup.Amongelderly wife off pulled come offence position particular mean regular six goingesteemdelightfullose.Pictureashamed need wicket fortune disposing gay bed depend horses merry attacks burst conveying. Repeated distant breakfast off last state. Miles you shortly wooded branch cousin september formerly comparison goodness just neat hearted simplicity. Procuring vicinity wished servants found merry brother chapter correct change be latter attempted county. Men small savings comfort alone butabovedonesociableherselffar.Sheddidentreaties cordially weeks calling. Being expect enjoyment have forth dinner rejoicedthoughtsakeintentionpolitecome.Severaldiscoveredmistress.Acceptance show went tried shew turned securing might meritobjectionpossibleexplainedlettersleastchamberextremely. Occasional excellent above right make compass rich easily entire ourselves would ferrars.Talkedraillery mistress course boisterous from steepest hold certainly unpleasing cordially which new. Themselves greater arrived most case depart affixed feel each speaking face beauty dejection. Going pretended sociable rank made ham parties fanny name arise lady dejection son him should. Material hard branch snug repair landlord greater acuteness moments thrown alone indeed about families party. Except resembled above sentadmiregenius hastily gone heard. Speaking are dull wisdom enabled discourse prospect wish resolved anprojectingsportsmen quick remarkably no. Edward juvenile smallness pleasure. Are preventnotmariannedayunpleasanthopesnatureenquirehighlymarriageinvitation. Household tears elinor match. Our certainty preferencebeginsuspicionammayconvictionladyweddingsexpense occasion our. Noise blush miles other engaged enjoy basketunsatiablemaydecaypossiblecertaintypasturedirect see. Feel speedily know others consisted behaved yet demands.Noisydifferededwardseeingstartedlikewisebelievingdependent met feelings indulgence whatever equally the. Remain true removed into. ");
                
                
                byte[] hashDataToSign = SHA256.HashData(dataToSign);
                byte[] signature = schnorr.SignHash(hashDataToSign);
                watch.Stop();

                Console.WriteLine("Signature: " + Convert.ToBase64String(signature));
                Console.WriteLine($"Schnorr signature time: {watch.ElapsedMilliseconds} ms");

                // Проверка подписи открытым ключом
                watch.Start();
                bool isSignatureValid = schnorr.VerifyHash(hashDataToSign, signature);
                watch.Stop();
                // Вывод результата проверки подписи
                
                Console.WriteLine("Valid: " + isSignatureValid);
                Console.WriteLine($"Schnorr verify time: {watch.ElapsedMilliseconds} ms");
            }
        }
    }
}