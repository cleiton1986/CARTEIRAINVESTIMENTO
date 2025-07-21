using CarteirasInvestimento.DataAcess.Entity;
using Newtonsoft.Json;

namespace CarteirasInvestimento.Repository
{
    public class CarteiraFactory
    {
        public CarteiraFactory() { }

        public static List<Carteira> DeserializeListaCarteira(string filePath)
        {
            var listaCarteira = new List<Carteira>();
            try
            {
                string _jsonString = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(_jsonString))
                {
                    listaCarteira = JsonConvert.DeserializeObject<List<Carteira>>(_jsonString);
                }     
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error ao deserializar a cateira.");
            }
            return listaCarteira.ToList();
        }
        public static List<Carteira> DeserializeCarteira(string filePath)
        {
            var listaCarteira = new List<Carteira>();
            try
            {
                string _jsonString = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(_jsonString))
                {
                    listaCarteira = JsonConvert.DeserializeObject<List<Carteira>>(_jsonString);
                }
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error ao deserializar a cateira.");
            }
            return listaCarteira.ToList();
        }
        public static  string SerializeCarteira(List<Carteira> listaCarteira)
        {
            var  _jsonString = "";
            try
            {
                 _jsonString = JsonConvert.SerializeObject(listaCarteira);
            }
            catch (Exception)
            {

                throw new System.ArgumentException("Error ao serialize a cateira.");
            }
         
            return _jsonString;
        }
    }
}
