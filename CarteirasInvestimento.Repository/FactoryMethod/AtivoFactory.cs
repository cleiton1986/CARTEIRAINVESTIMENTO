using CarteirasInvestimento.DataAcess.Entity;
using Newtonsoft.Json;

namespace CarteirasInvestimento.Repository
{
    public class AtivoFactory
    {

        public static List<Ativo> DeserializeListaAtivo(string filePath)
        {
            var listaAtivo = new List<Ativo>();
            try
            {
                string _jsonString = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(_jsonString))
                {
                    listaAtivo = JsonConvert.DeserializeObject<List<Ativo>>(_jsonString);
                }
              
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error ao deserializar a ativo.");
            }
            return listaAtivo.ToList();
        }
        public static string SerializeAtivo(List<Ativo> listaAtivo)
        {
            var _jsonString = "";
            try
            {
                _jsonString = JsonConvert.SerializeObject(listaAtivo);
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException($"Error ao serialize a ativo.: {ex.Message}");
            }

            return _jsonString;
        }
    }
}
