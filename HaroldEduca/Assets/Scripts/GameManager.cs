using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using Universe;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System;
using Newtonsoft.Json;

[RequireComponent(typeof(AudioSource))]
public class GameManager : Manager<GameManager>
{
    //Para la escena Partida
    [SerializeField]private AudioClip correctSong = null;
    [SerializeField]private AudioClip incorrectSong = null;
    [SerializeField]private AudioClip musicaFondo = null;
    [SerializeField]private Color correctColor = Color.black;
    [SerializeField]private Color incorrectColor = Color.black;
    [SerializeField]private float waitTime = 0.0f;

    private Text score_text = null;
    private Outline outlineUsername, outlinePassword, outlineEmail;
    private int score = 0;
    private static string IP_SERVER = "haroldeduca.000webhostapp.com";
    private GameObject PanelError = null;
    private GameObject PanelBloqueante = null;

    private bool hasConection;
    private WebClient client = null;
    private Stream stream = null;

    private QuizDB quizDB = null;
    private QuizUI quizUI;
    private AudioSource audioSource = null;
    public AudioSource audioSourceFondo = null;
    private int aciertos, fallos;
    private string categoria_partida = "";

    private float musicVolumen = 1f;

    private string nombre_usuario = "";

    void Start()
    {
        CambiarEscena("Cargando");
        audioSource = GetComponent<AudioSource>();
        audioSourceFondo.Play();
        aciertos = 0;
        fallos = 0;
        client = new WebClient();
    }

    private void Update()
    {
        audioSourceFondo.volume = musicVolumen;
        audioSource.volume = musicVolumen;
    }

    public void ComprobarInicioSesion()
    {
        string user = PlayerPrefs.GetString("LastUserLoggin");
        if (user == null || user == "")
        {
            CambiarEscena("LogIn");
        }
        else
        {
            if (GetLoggedUser(user))
            {
                CambiarEscena("Menu");
                nombre_usuario = user;
            }
            else
            {
                CambiarEscena("LogIn");
            }
        }
    }

    private bool GetLoggedUser(string last_user) // comprobar si el usuario guardado existe en la bd
    {
        string respuesta = "";
        bool existe = false;
        string uri = "http://" + IP_SERVER + "/proyecto/GetLoggedUser.php";
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format("loggedUser={0}", last_user));

            WebRequest request = WebRequest.Create(String.Format(uri));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            respuesta = sr.ReadToEnd();
            if (respuesta.Equals("1"))
            {
                existe = true;
            }
            else
            {
                existe = false;
            }

            sr.Close();
            stream.Close();

        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return existe;
    }

    public bool CheckConexion() {

        try {
            stream = client.OpenRead("http://www.google.com");
            stream.Close();
            return true;
        }
        catch {

            return false;
        }
    }

    public void GetPanelError(GameObject Panel) {
        PanelError = Panel;
    }

    //Es el panel que se activa cuando se selecciona una respuesta y no se pueda seleccionar mas de una
    public void GetPanelBloqueante(GameObject PanelBq)
    {
        PanelBloqueante = PanelBq;
    }


    public void comprobarCredencialesLogIn(InputField username_field, InputField password_field, Text m_error) {
        outlineUsername = username_field.GetComponent<Outline>();
        outlinePassword = password_field.GetComponent<Outline>();
        string response = "";

        if (username_field.text.Length < 4 && password_field.text.Length < 4)
        {
            m_error.text = "Completa los campos";
            outlineUsername.effectColor = Color.red;
            outlineUsername.effectDistance = new Vector2(4, 4);
            outlinePassword.effectColor = Color.red;
            outlinePassword.effectDistance = new Vector2(4, 4);
        }
        else if (username_field.text.Length < 4)
        {
            m_error.text = "";
            outlineUsername.effectColor = Color.red;
            outlineUsername.effectDistance = new Vector2(2, 2);
            outlinePassword.effectColor = Color.black;
            outlinePassword.effectDistance = new Vector2(1, -1);
            m_error.text = "Completa el campo Username";
        }
        else if (password_field.text.Length < 4)
        {
            m_error.text = "";
            outlinePassword.effectColor = Color.red;
            outlinePassword.effectDistance = new Vector2(2, 2);
            outlineUsername.effectColor = Color.black;
            outlineUsername.effectDistance = new Vector2(1, -1);
            m_error.text = "Completa el campo Password";
        }
        else
        {
            response = LogInUser(username_field, password_field, m_error);
            if (response.Equals("Usuario no existe"))
            {
                m_error.text = response;
            }
            else if (response.Equals("Password incorrecto"))
            {
                m_error.text = response;
            }
            else if (response.Equals("Login success"))
            {
                CambiarEscena("Menu");
                nombre_usuario = username_field.text;
                PlayerPrefs.SetString("LastUserLoggin",username_field.text);
            }
            else {
                Debug.Log("ERROR:" + response);
            }
        }
    }

    private string LogInUser(InputField username_field, InputField password_field, Text m_error)
    {
        string username = username_field.text;
        string password_encripted = Encriptar(password_field.text);
        string uri = "http://" + IP_SERVER + "/proyecto/Login.php";
        string respuesta = "";

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format("loginUser={0}&loginPass={1}", username, password_encripted));

            WebRequest request = WebRequest.Create(String.Format(uri));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            respuesta = sr.ReadToEnd();

            sr.Close();
            stream.Close();
            Debug.Log(respuesta);

        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return respuesta;
    }


    public void comprobarCredencialesRegistro(InputField username_field, InputField password_field, Text m_error)
    {
        outlineUsername = username_field.GetComponent<Outline>();
        outlinePassword = password_field.GetComponent<Outline>();
        string response = "";

        if (username_field.text.Length < 4 && password_field.text.Length < 4)
        {
            m_error.text = "Completa los campos";
            outlineUsername.effectColor = Color.red;
            outlineUsername.effectDistance = new Vector2(2, 2);
            outlinePassword.effectColor = Color.red;
            outlinePassword.effectDistance = new Vector2(2, 2);
        }
        else if (username_field.text.Length < 4)
        {
            m_error.text = "";
            outlineUsername.effectColor = Color.red;
            outlineUsername.effectDistance = new Vector2(2, 2);
            outlinePassword.effectColor = Color.black;
            outlinePassword.effectDistance = new Vector2(1, -1);
            m_error.text = "Completa el campo Username";
        }
        else if (password_field.text.Length < 4)
        {
            m_error.text = "";
            outlinePassword.effectColor = Color.red;
            outlinePassword.effectDistance = new Vector2(2, 2);
            outlineUsername.effectColor = Color.black;
            outlineUsername.effectDistance = new Vector2(1, -1);
            m_error.text = "Completa el campo Password";
        }
        else
        {
            response = RegisterUser(username_field, password_field, m_error);
            if (response.Equals("Username ya existe. Intentelo de nuevo"))
            {
                m_error.text = "Username ya existe.Intentelo de nuevo";
                username_field.text = "";
                password_field.text = "";
            }
            else if (response.Equals("1"))
            {
                LogInUser(username_field, password_field, m_error);
                CambiarEscena("Menu");
                nombre_usuario = username_field.text;
                PlayerPrefs.SetString("LastUserLoggin", username_field.text);
            }
            else if (response.Equals("0")) {
                m_error.text = "No se ha podido crear el usuario correctamente. Intentelo de nuevo";
                username_field.text = "";
                password_field.text = "";
            }
            else
            {
                Debug.Log(response);
                m_error.text = "No se ha podido crear el usuario correctamente. Intentelo de nuevo";
            }
        }
    }

    private string RegisterUser(InputField username_field, InputField password_field, Text m_error)
    {
        string username = username_field.text;
        string password_encripted = Encriptar(password_field.text);
        string uri = "http://" + IP_SERVER + "/proyecto/RegisterUser.php";
        string respuesta = "";

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format("RegisterUser={0}&RegisterPass={1}", username, password_encripted));

            WebRequest request = WebRequest.Create(String.Format(uri));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            respuesta = sr.ReadToEnd();

            sr.Close();
            stream.Close();

        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return respuesta;
    }

    public IEnumerator GetCategoriasBien(string catPadre, GameObject btnTemplate)
    {
        List<Categoria> lsSubCategorias = new List<Categoria>();
        string uri = "http://" + IP_SERVER + "/proyecto/GetCategoriasBien.php";
        WWWForm form = new WWWForm();
        form.AddField("catPadre", catPadre);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Equals("0"))
                {
                    StartCoroutine(GetPreguntas(catPadre));
                }
                else
                {
                    string json = www.downloadHandler.text;
                    if (json == "0")
                    {
                        Debug.Log("No hay cateogrias");
                    }
                    else {
                        try
                        {
                            Debug.Log("JSON:" + json);
                            lsSubCategorias = JsonConvert.DeserializeObject<List<Categoria>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                            //Creación de los botones
                            for (int i = 0; i <= lsSubCategorias.Count - 1; i++)
                            {
                                GameObject btn = Instantiate(btnTemplate) as GameObject;
                                btn.SetActive(true);
                                btn.GetComponent<BtnListButton>().SetText(lsSubCategorias[i].Nombre_cat);
                                btn.transform.SetParent(btnTemplate.transform.parent, false);
                            }
                        }
                        catch (Exception e) {
                            Debug.Log(e.ToString());
                        }

                    }
                }
            }
        }
    }

    public void GetCategorias(string catPadre, GameObject btnTemplate)
    {
        List<Categoria> lsCategorias = null;
        try
        {
            byte[] data = Encoding.UTF8.GetBytes("catPadre=" + catPadre);
            WebRequest request = WebRequest.Create(String.Format("http://" + IP_SERVER + "/proyecto/GetCategoriasBien.php"));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            string response_json = sr.ReadToEnd();
            Debug.Log("ERROR: " + response_json);

            sr.Close();
            stream.Close();
            if (response_json == "0")
            {
                lsCategorias = null;
            }
            else
            {
                lsCategorias = JsonConvert.DeserializeObject<List<Categoria>>(response_json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                foreach (Categoria c in lsCategorias)
                {
                    Debug.Log(c.ToString());
                    GameObject btn = Instantiate(btnTemplate) as GameObject;
                    btn.SetActive(true);
                    btn.GetComponent<BtnListButton>().SetText(c.Nombre_cat);
                    btn.transform.SetParent(btnTemplate.transform.parent, false);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
    }

    public IEnumerator GetPreguntas(string nombreCatPadre)
    {
        List<Pregunta> lsPreguntas = null;
        string uri = "http://" + IP_SERVER + "/proyecto/GetPreguntas.php";
        WWWForm form = new WWWForm();
        form.AddField("nombreCatPadre", nombreCatPadre);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Equals("0"))
                {
                    Debug.Log(www.downloadHandler.text);
                    PanelError.SetActive(true);
                }
                else
                {
                    string json = www.downloadHandler.text;
                    lsPreguntas = JsonConvert.DeserializeObject<List<Pregunta>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    foreach (Pregunta p in lsPreguntas)
                    {
                        List<Respuesta> lsRespuestas = null;
                        lsRespuestas = ObtenerRespuestas(p.Id_pregunta);
                        p.Respuestas = lsRespuestas;
                    }
                    cambiarEscenaPartida(new QuizDB(lsPreguntas), nombreCatPadre);
                }
            }
        }
    }


    private List<Respuesta> ObtenerRespuestas(int id_pregunta)
    {
        List<Respuesta> resultado = null;
        try
        {
            byte[] data = Encoding.UTF8.GetBytes("id_pregunta=" + id_pregunta);

            WebRequest request = WebRequest.Create(String.Format("http://" + IP_SERVER + "/proyecto/GetRespuestas.php"));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();

            sr.Close();
            stream.Close();

            resultado = JsonConvert.DeserializeObject<List<Respuesta>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return resultado;
    }

    public List<Partida> GetRanking()
    {
        List<Partida> lsPartidas = null;
        try
        {
            WebRequest request = WebRequest.Create(String.Format("http://" + IP_SERVER + "/proyecto/GetRanking.php"));
            request.Method = "POST";

            Stream stream = request.GetRequestStream();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            string response_json = sr.ReadToEnd();

            sr.Close();
            stream.Close();
            if (response_json == "0")
            {
                lsPartidas = null;
            }
            else {
                lsPartidas = JsonConvert.DeserializeObject<List<Partida>>(response_json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
            lsPartidas = null;
        }
        return lsPartidas;
    }

    private void cambiarEscenaPartida(QuizDB qdb, string cat) {
        quizDB = qdb;
        quizDB.Awake();
        CambiarEscena("Partida");
        categoria_partida = cat;
        audioSourceFondo.Stop();
    }

    public void NextPregunta(QuizUI q) {
        quizUI = q;
        if (quizDB.ListaPreguntas.Count == 0)
        {
            audioSourceFondo.Play();
            CambiarEscena("ResumenPartida");
        }
        else {
            quizUI.Constructor(quizDB.GetRandom(), GiveAnswer);
        }

    }

    private void GiveAnswer(OptionButton optionButton) {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    private IEnumerator GiveAnswerRoutine(OptionButton optionButton)
    {
        PanelBloqueante.SetActive(true);
        if (audioSource.isPlaying)
            audioSource.Stop();

        bool Rcorrecta = false;
        if (optionButton.Respuesta.Correcta == 1) { // Mysql devuelve un 1 o un 0, hay que transformarlo
            Rcorrecta = true;
        }

        audioSource.clip = Rcorrecta ? correctSong : incorrectSong;
        optionButton.SetColor(Rcorrecta ? correctColor : incorrectColor);

        if (Rcorrecta)
        {
            aciertos += 1;
            score = score + 100;
            SetScore(score);
        }
        else {
            fallos += 1;
            score = score - 100;
            if (score < 0) {
                score = 0;
            }
            SetScore(score);
        }


        audioSource.Play();
        yield return new WaitForSeconds(waitTime);
        PanelBloqueante.SetActive(false);
        NextPregunta(quizUI);
    }

    public string GuardarPartida(string username, string score)
    {
        int puntos = int.Parse(score);
        int id_categoria = GetIdCategoria(categoria_partida); //obtener el id de la categoria a partir del nombre
        string uri = "http://" + IP_SERVER + "/proyecto/GuardarPartida.php";
        string respuesta = "";
        if (id_categoria == 0) //hubo un error en la inserccion devolver el 0 y en el ResumenPartida tratarlo mostando un mensaje
        {
            respuesta = "0";
        }
        else
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(String.Format("username={0}&puntos={1}&id_categoria={2}", username, puntos, id_categoria));

                WebRequest request = WebRequest.Create(String.Format(uri));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                WebResponse response = request.GetResponse();
                stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream);
                respuesta = sr.ReadToEnd();

                sr.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
            }

        }
        return respuesta;
    }

    private int GetIdCategoria(string categoria)
    {
        string uri = "http://" + IP_SERVER + "/proyecto/GetIdCategoria.php";
        string respuesta = "";
        int id_categoria = 0;

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format("categoria={0}", categoria));

            WebRequest request = WebRequest.Create(String.Format(uri));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            respuesta = sr.ReadToEnd();
            id_categoria = int.TryParse(respuesta, out id_categoria) ? id_categoria : 0;

            sr.Close();
            stream.Close();

        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return id_categoria;
    }

    public string GetNombreCategoria(int id_categoria)
    {
        string uri = "http://" + IP_SERVER + "/proyecto/GetNombreCategoria.php";
        string respuesta = "";

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format("id_cat={0}", id_categoria));

            WebRequest request = WebRequest.Create(String.Format(uri));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            respuesta = sr.ReadToEnd();

            sr.Close();
            stream.Close();

        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
        return respuesta;
    }


    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void ActualizarNombre(Text usuario) {
        usuario.text = nombre_usuario;
    }

    public static string Encriptar(string pass)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(pass);
        result = Convert.ToBase64String(encryted);
        return result;
    }

    public static string DesEncriptar(string passEncriptado)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(passEncriptado);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }

    public void SetVolumen(float vol) {
        musicVolumen = vol;
    }

    public float GetVolumen()
    {
        return musicVolumen;
    }

    public void GetScore(Text Score) {
        score_text = Score;
        score = Int32.Parse(Score.text);

    }

    public void SetScore(int score) {
        score_text.text = score.ToString();
    }

    public int[] GetAciertoFallos() {
        int[] aciertosFallos = { aciertos, fallos, score };
        return aciertosFallos;
    }

    public void setAciertosFallos() {
        aciertos = 0;
        fallos = 0;
        score = 0;
    }
}
