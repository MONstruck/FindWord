using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using WebApplication4.Models;


namespace WebApplication4
{
    public class Logic
    {


        private static List<string> FindSentence(string textFile)
        {
            List<string> sentences = new List<string>();
            string[] sentence = textFile.Split('.');
            for (int i = 0; i < sentence.Length-1; i++)
            {
                if (sentence[i] != null)
                {
                    sentences.Add(sentence[i]);
                }
            }
            return sentences;
        }

        private static List<string> FindWord(string textFile, string word)
        {

            List<string> sentece = new List<string>();
            char[] reverseSentence;
            foreach (var sentence in FindSentence(textFile))
            {
                reverseSentence = sentence.ToArray();
                Array.Reverse(reverseSentence);
                string reverse = new String(reverseSentence);
                sentece.Add(reverse);
            }


            return sentece;
        }

        private static uint Numbers(string sentnce, string word)
        {
            char[] reverseWord;
            reverseWord = word.ToArray();
            Array.Reverse(reverseWord);
            word = new string(reverseWord);

            uint counter = 0;
            string[] sentence = sentnce.Split(' ');
            for (int i = 0; i < sentence.Length; i++)
            {
                if (word == sentence[i])
                {
                    counter++;
                }
            }

            return counter;
        }


        public static void ConvertToJson(string textFile, string word)
        {
            List<SentenceModel> sentenceModelsList = new List<SentenceModel>();

            foreach (var sentece in FindWord(textFile, word))
            {
                SentenceModel sentenceModel = new SentenceModel()
                {
                    Word = word,
                    Sentence = sentece,
                    Number = Numbers(sentece, word)

                };
                sentenceModelsList.Add(sentenceModel);
            }
            string add = File.ReadAllText(HttpContext.Current.Server.MapPath("Data/jsonFile.json"));
            var back = JsonConvert.DeserializeObject<List<SentenceModel>>(add);

            sentenceModelsList.AddRange(back);

            var json = JsonConvert.SerializeObject(sentenceModelsList);

            File.WriteAllText(HttpContext.Current.Server.MapPath("Data/jsonFile.json"), json);

        }

         public static List<SentenceModel> ConvertOut()
         {
            
             List<SentenceModel> sentenceModelsList = new List<SentenceModel>();
            string add="";
            try
            {
                 add = File.ReadAllText(HttpContext.Current.Server.MapPath("Data/jsonFile.json"));
            }
            catch { }
                var back = JsonConvert.DeserializeObject<List<SentenceModel>>(add);
            if (back != null)
            {
                Reverese(back);
                sentenceModelsList.AddRange(back);
                
            }
            return sentenceModelsList;
        }
        private static List<SentenceModel> Reverese(List<SentenceModel> sentenceModelsList)
        {
            char[] reverseSentence;
            foreach (var sentence in sentenceModelsList)
            {
                reverseSentence = sentence.Sentence.ToArray();
                Array.Reverse(reverseSentence);
                string reverse = new String(reverseSentence);
                sentence.Sentence = reverse;
            }
            return sentenceModelsList;
        }

    }
}