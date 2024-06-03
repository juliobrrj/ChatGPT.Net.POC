using ChatGPT.Net;
using System.Speech.Synthesis;
using System.Text.Json;


string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
string chaveGPT = string.Empty;
    try
    {
        string jsonString = File.ReadAllText(caminhoArquivo);

        var appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);

        if (appSettings != null)
        {
            chaveGPT = appSettings.chaveGPT;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao ler o arquivo JSON: {ex.Message}");
    }



// ChatGPT Official API
var bot = new ChatGpt(chaveGPT);

bot.ClearConversations();

string salaChat = new Guid().ToString();

var prompt = string.Empty;
Console.WriteLine("============= POC TJERJ ==============");
Console.WriteLine("");
string retorno;
retorno = await bot.Ask(@"COMANDO", salaChat);
//Console.WriteLine(retorno);
//Task.Run(() => DizerFala(retorno));

String complemento =  string.Empty;

while (true)
{


    Console.Write("Digite a palavra: ");

    prompt = Console.ReadLine();
    Console.Write(" ");
    Console.Write("Resposta: ");
    retorno = await bot.Ask(prompt, salaChat);
    Console.WriteLine(retorno);
    //Task.Run(() => DizerFala(retorno));

    Console.WriteLine();
}

async Task DizerFala(string texto)
{
    SpeechSynthesizer synth = new SpeechSynthesizer();
    synth.SetOutputToDefaultAudioDevice();
    synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
    synth.Rate = 3;
    synth.Speak(texto);
}
