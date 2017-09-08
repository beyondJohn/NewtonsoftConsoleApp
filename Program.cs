using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CreateChunckedAlphabetizedCityLists
{
    class Program
    {
        static void Main(string[] args)
        {
            createFiles();
        }
        static void createFiles()
        {
            JObject o1 = JObject.Parse(File.ReadAllText(@"C:\Users\john\Documents\Visual Studio 2017\Projects\CreateChunckedAlphabetizedCityLists\CreateChunckedAlphabetizedCityLists\TextFile1.txt"));
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            foreach (var alpha in alphabet)
            {
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(@"C:\Users\joselowpc\Documents\Visual Studio 2017\Projects\CreateChunckedAlphabetizedCityLists\CreateChunckedAlphabetizedCityLists\TextFile1.txt"))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        foreach (var obj in o2)
                        {
                            JArray array = JArray.Parse(obj.Value.ToString());
                            string buildFile = "[";
                            foreach (JObject content in array.Children<JObject>())
                            {
                                foreach (JProperty prop in content.Properties())
                                {
                                    string myVal = prop.Value.ToString();
                                    if (myVal.StartsWith(alpha))
                                    {
                                        buildFile += "{citystate:'" + prop.Value.ToString() + "'},";
                                    }
                                    //Console.WriteLine(prop.Value);
                                    //Console.WriteLine(prop.Name);
                                }
                            }
                            buildFile += "]";
                            //remove last comma from created file
                            buildFile = buildFile.Remove(buildFile.Length - 2, 1);
                            File.WriteAllText(@"C:\Users\john\Documents\Visual Studio 2017\Projects\CreateChunckedAlphabetizedCityLists\CreateChunckedAlphabetizedCityLists\AlphaFilter\" + alpha + "States.txt", buildFile);
                        }
                    }
                }
            }
        }
    }
}
