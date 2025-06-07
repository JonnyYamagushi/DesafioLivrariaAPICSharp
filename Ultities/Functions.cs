namespace DesafioLivrariaAPICSharp.Ultities;

/// <summary>
/// Classe contendo métodos utilitários para operações diversas, como verificação de valores numéricos, datas e etc.
/// </summary>
public class Functions
{
    /// <summary>
    /// Escreve uma mensagem de log em um arquivo de log específico para o método.
    /// </summary>
    /// <param name="metodo">O nome do método que gerou o log.</param>
    /// <param name="msg">A mensagem a ser escrita no log.</param>
    public static void EscreveLog(string metodo, string msg)
    {
        string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs", metodo);
        string logPath = Path.Combine(logDirectory, $"Log_{DateTime.Now:ddMMyyyyhhmmssfff}.txt");

        try
        {
            // Verifica se o diretório existe e cria se não existir
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Escreve no arquivo de log
            using (StreamWriter sw = new StreamWriter(logPath, false))
            {
                sw.Write(msg);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
        }
    }
}
