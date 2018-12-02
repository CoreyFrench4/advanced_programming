using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class Login : MonoBehaviour
{

    // Use this for initialization
    float scrW = Screen.width / 16;
    float scrH = Screen.height / 9;
    public bool isLogin = true;
    public bool isCreateAccount, isReset;
    public string username, email, password = "";
    public string debuguser;
    private string notif;
    public string code, newPass, confirmPass, inputCode;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void GenerateCode()
    {
        int desiredCodeLength = 8;
        code = "";
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        while (code.Length < desiredCodeLength)
        {
            code += characters[UnityEngine.Random.Range(0, 58)];
        }
        Debug.Log("Random code: " + code);
    }
    void OnGUI()
    {

        if (isLogin == true)
        {

            if (GUI.Button(new Rect(2 * scrW, 2.5f * scrH, 6 * scrW, scrH), "Login"))
            {
                StartCoroutine(login(username, password));
            }
            if (GUI.Button(new Rect(2 * scrW, 4f * scrH, 6 * scrW, scrH), "Create"))
            {
                isLogin = false;
            }
            if (GUI.Button(new Rect(2 * scrW, 5.5f * scrH, 6 * scrW, scrH), "SendEmail"))
            {
                StartCoroutine(CheckEmail(email));
                isReset = true;
                isLogin = false;
           
            }
            
            GUI.skin.textArea.wordWrap = true;
            GUI.Box(new Rect(10 * scrW, 7f * scrH, 6 * scrW, scrH), notif);
            username = GUI.TextField(new Rect(10 * scrW, 2.5f * scrH, 6 * scrW, scrH), username, 32);
            email = GUI.TextField(new Rect(10 * scrW, 4f * scrH, 6 * scrW, scrH), email, 32);
            password = GUI.PasswordField(new Rect(10 * scrW, 5.5f * scrH, 6 * scrW, scrH), password, "*"[0], 25);
        }
        if (isCreateAccount == true)
        {
            if (GUI.Button(new Rect(2 * scrW, 4f * scrH, 6 * scrW, scrH), "Create"))
            {
                StartCoroutine(CreateAccount(username, email, password));
                isLogin = true;
                isCreateAccount = false;
            }
            GUI.skin.textArea.wordWrap = true;
            GUI.Box(new Rect(10 * scrW, 7f * scrH, 6 * scrW, scrH), notif);
            username = GUI.TextField(new Rect(10 * scrW, 2.5f * scrH, 6 * scrW, scrH), username, 32);
            email = GUI.TextField(new Rect(10 * scrW, 4f * scrH, 6 * scrW, scrH), email, 32);
            password = GUI.PasswordField(new Rect(10 * scrW, 5.5f * scrH, 6 * scrW, scrH), password, "*"[0], 25);
        }
        if (isReset == true)
        {
            if (GUI.Button(new Rect(2 * scrW, 7f * scrH, 6 * scrW, scrH), "Reset"))
            {
                if (newPass == confirmPass && inputCode == code)
                    StartCoroutine(ResetPassword(email, newPass));

            }
            GUI.skin.textArea.wordWrap = true;
            GUI.Box(new Rect(10 * scrW, 7f * scrH, 6 * scrW, scrH), notif);
            inputCode = GUI.TextField(new Rect(10 * scrW, 2.5f * scrH, 6 * scrW, scrH), inputCode, 32);
            newPass = GUI.PasswordField(new Rect(10 * scrW, 4f * scrH, 6 * scrW, scrH), newPass, "*"[0], 25);
            confirmPass = GUI.PasswordField(new Rect(10 * scrW, 5.5f * scrH, 6 * scrW, scrH), confirmPass, "*"[0], 25);
        }
    }
    IEnumerator CreateAccount(string username, string email, string password)
    {
        string createUserURL = "http://localhost/sqlsystem/createuser.php";
        WWWForm user = new WWWForm();
        user.AddField("username_Post", username);
        user.AddField("email_Post", email);
        user.AddField("password_Post", password);
        WWW www = new WWW(createUserURL, user);
        yield return www;
        Debug.Log(www.text);

        #region Notification
        notif = www.text;
        #endregion
    }
    IEnumerator login(string username, string password)
    {
        string LoginURL = "http://localhost/sqlsystem/Login.php";
        WWWForm user = new WWWForm();
        user.AddField("username_Post", username);
        user.AddField("password_Post", password);
        WWW www = new WWW(LoginURL, user);
        yield return www;
        Debug.Log(www.text);

        #region Notification
        notif = www.text;
        #endregion
        if (notif == "Login Success")
        {
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator ResetPassword(string email, string newPass)
    {
        string LoginURL = "http://localhost/sqlsystem/ResetPassword.php";
        WWWForm passWord = new WWWForm();

        passWord.AddField("email_Post", email);
        passWord.AddField("password_Post", newPass);
        WWW www = new WWW(LoginURL, passWord);
        yield return www;
        Debug.Log(www.text);
        
        #region Notification
        notif = www.text;
        #endregion
    }
    IEnumerator CheckEmail(string email)
    {
        string LoginURL = "http://localhost/sqlsystem/CheckEmail.php";
        WWWForm eMail = new WWWForm();

        eMail.AddField("email_Post", email);
        WWW www = new WWW(LoginURL, eMail);
        yield return www;
        Debug.Log(www.text);
        if (www.text != "User Not Found")
        {
            GenerateCode();
            SendEmail(email, www.text);
        }
        #region Notification
        notif = www.text;
        #endregion
    }
    void SendEmail(string email, string debuguser)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(email);
        mail.Subject = "Password Reset";
        mail.Body = "Hello " + debuguser + "\n Reset using this code: " + code;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new System.Net.NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;

        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        { return true; };
        smtpServer.Send(mail);
        Debug.Log("Sending Email");
    }


}
