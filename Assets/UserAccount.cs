using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;  
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;



// ユーザ関数の定義
// 投げる物
/*
* body{"user_name":"text","password":"text","mode":"create"or"login"}
*/

public class UserAccount : MonoBehaviour
{
    string rootUrl = "https://fmia45kh59.execute-api.ap-northeast-3.amazonaws.com/dev";    

    public InputField inputUserName;
    public InputField inputPassword;
    public InputField inputPasswordConfirm;
    string userName;
    string password;
    string passwordConfirm;
    string mode="login";
    // IEnumerator GetRequest(string url)
    // {
    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();

    //     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //     {
    //         Debug.LogError(request.error);
    //     }
    //     else
    //     {
    //         Debug.Log("GET Response: " + request.downloadHandler.text);
    //     }
    // }

    IEnumerator GetUserRequest(string url,string mode , string userName, string password)
    {
      Debug.Log("mode: "+mode);
      Debug.Log("userName: "+userName);
      Debug.Log("password: "+password);
      Debug.Log("url: "+url);

      url = url + "/user";
        // JSONデータを作成
      string jsonString = $"{{\"body\": {{\"user_name\": \"{userName}\",\"password\": \"{password}\",\"mode\": \"{mode}\"}}}}";
        
      // UnityWebRequestを使用してPOSTリクエストを作成
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

      Debug.Log("jsonString: "+jsonString);
      Debug.Log("request");
      // リクエストを送信してレスポンスを待つ
      yield return request.SendWebRequest();
    
      if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
      {
          Debug.LogError($"POST Error: {request.error}");
      }
      else
      {
          Debug.Log("POST Response: " + request.downloadHandler.text);

        // 関数を挿入
        cheackJson(request);
        
      }
    }

//     IEnumerator PostRequest(string url)
//     {
//         // JSONデータを作成
//         // string jsonString = @"{
//   // ""body"": {
//   //   ""user_id"": ""12341234"",
//   //   ""point"": 123
//   // },
//   // ""resource"": ""/{proxy+}"",
//   // ""path"": ""/path/to/resource"",
//   // ""httpMethod"": ""POST"",
//   // ""isBase64Encoded"": true,
//   // ""queryStringParameters"": {
//   //   ""foo"": ""bar""
//   // },
//   // ""multiValueQueryStringParameters"": {
//   //   ""foo"": [
//   //     ""bar""
//   //   ]
//   // },
//   // ""pathParameters"": {
//   //   ""proxy"": ""/path/to/resource""
//   // },
//   // ""stageVariables"": {
//   //   ""baz"": ""qux""
//   // },
//   // ""headers"": {
//   //   ""Accept"": ""text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"",
//   //   ""Accept-Encoding"": ""gzip, deflate, sdch"",
//   //   ""Accept-Language"": ""en-US,en;q=0.8"",
//   //   ""Cache-Control"": ""max-age=0"",
//   //   ""CloudFront-Forwarded-Proto"": ""https"",
//   //   ""CloudFront-Is-Desktop-Viewer"": ""true"",
//   //   ""CloudFront-Is-Mobile-Viewer"": ""false"",
//   //   ""CloudFront-Is-SmartTV-Viewer"": ""false"",
//   //   ""CloudFront-Is-Tablet-Viewer"": ""false"",
//   //   ""CloudFront-Viewer-Country"": ""US"",
//   //   ""Host"": ""1234567890.execute-api.us-east-1.amazonaws.com"",
//   //   ""Upgrade-Insecure-Requests"": ""1"",
//   //   ""User-Agent"": ""Custom User Agent String"",
//   //   ""Via"": ""1.1 08f323deadbeefa7af34d5feb414ce27.cloudfront.net (CloudFront)"",
//   //   ""X-Amz-Cf-Id"": ""cDehVQoZnx43VYQb9j2-nvCh-9z396Uhbp027Y2JvkCPNLmGJHqlaA=="",
//   //   ""X-Forwarded-For"": ""127.0.0.1, 127.0.0.2"",
//   //   ""X-Forwarded-Port"": ""443"",
//   //   ""X-Forwarded-Proto"": ""https""
//   // },
//   // ""multiValueHeaders"": {
//   //   ""Accept"": [
//   //     ""text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8""
//   //   ],
//   //   ""Accept-Encoding"": [
//   //     ""gzip, deflate, sdch""
//   //   ],
//   //   ""Accept-Language"": [
//   //     ""en-US,en;q=0.8""
//   //   ],
//   //   ""Cache-Control"": [
//   //     ""max-age=0""
//   //   ],
//   //   ""CloudFront-Forwarded-Proto"": [
//   //     ""https""
//   //   ],
//   //   ""CloudFront-Is-Desktop-Viewer"": [
//   //     ""true""
//   //   ],
//   //   ""CloudFront-Is-Mobile-Viewer"": [
//   //     ""false""
//   //   ],
//   //   ""CloudFront-Is-SmartTV-Viewer"": [
//   //     ""false""
//   //   ],
//   //   ""CloudFront-Is-Tablet-Viewer"": [
//   //     ""false""
//   //   ],
//   //   ""CloudFront-Viewer-Country"": [
//   //     ""US""
//   //   ],
//   //   ""Host"": [
//   //     ""0123456789.execute-api.us-east-1.amazonaws.com""
//   //   ],
//   //   ""Upgrade-Insecure-Requests"": [
//   //     ""1""
//   //   ],
//   //   ""User-Agent"": [
//   //     ""Custom User Agent String""
//   //   ],
//   //   ""Via"": [
//   //     ""1.1 08f323deadbeefa7af34d5feb414ce27.cloudfront.net (CloudFront)""
//   //   ],
//   //   ""X-Amz-Cf-Id"": [
//   //     ""cDehVQoZnx43VYQb9j2-nvCh-9z396Uhbp027Y2JvkCPNLmGJHqlaA==""
//   //   ],
//   //   ""X-Forwarded-For"": [
//   //     ""127.0.0.1, 127.0.0.2""
//   //   ],
//   //   ""X-Forwarded-Port"": [
//   //     ""443""
//   //   ],
//   //   ""X-Forwarded-Proto"": [
//   //     ""https""
//   //   ]
//   // },
//   // ""requestContext"": {
//   //   ""accountId"": ""123456789012"",
//   //   ""resourceId"": ""123456"",
//   //   ""stage"": ""prod"",
//   //   ""requestId"": ""c6af9ac6-7b61-11e6-9a41-93e8deadbeef"",
//   //   ""requestTime"": ""09/Apr/2015:12:34:56 +0000"",
//   //   ""requestTimeEpoch"": 1428582896000,
//   //   ""identity"": {
//   //     ""cognitoIdentityPoolId"": null,
//   //     ""accountId"": null,
//   //     ""cognitoIdentityId"": null,
//   //     ""caller"": null,
//   //     ""accessKey"": null,
//   //     ""sourceIp"": ""127.0.0.1"",
//   //     ""cognitoAuthenticationType"": null,
//   //     ""cognitoAuthenticationProvider"": null,
//   //     ""userArn"": null,
//   //     ""userAgent"": ""Custom User Agent String"",
//   //     ""user"": null
//   //   },
//   //   ""path"": ""/prod/path/to/resource"",
//   //   ""resourcePath"": ""/{proxy+}"",
//   //   ""httpMethod"": ""POST"",
//   //   ""apiId"": ""1234567890"",
//   //   ""protocol"": ""HTTP/1.1""
//   // }
// // }";
//         string jsonString = @"{";

//         // UnityWebRequestを使用してPOSTリクエストを作成
//         UnityWebRequest request = new UnityWebRequest(url, "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
//         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = new DownloadHandlerBuffer();
//         request.SetRequestHeader("Content-Type", "application/json");

//         // リクエストを送信してレスポンスを待つ
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//         {
//             Debug.LogError($"POST Error: {request.error}");
//         }
//         else
//         {
//             Debug.Log("POST Response: " + request.downloadHandler.text);
//         }
//     }

    //入力された名前情報を読み取ってコンソールに出力する関数
    //InputFieldのOnEndEditに登録する
    public void GetInputUserName()
    {
        //InputFieldからテキスト情報を取得する
        userName = inputUserName.text;
        Debug.Log("name: " + userName);
        //ここでエラーチェックしてもいいかもしれない
        //入力フォームのテキストを空にする
        // inputUserName.text = "";
    }

    //入力されたPassword情報を読み取ってコンソールに出力する関数
    //InputFieldのOnEndEditに登録する
    public void GetInputPassword()
    {
        //InputFieldからテキスト情報を取得する
        password = inputPassword.text;
        Debug.Log("password:"+password);
        //ここでエラーチェックしてもいいかもしれない
        //入力フォームのテキストを空にする
    }

    //入力されたPassword情報を読み取ってコンソールに出力する関数
    //InputFieldのOnEndEditに登録する
    public void GetInputPasswordConfirm()
    {
        //InputFieldからテキスト情報を取得する
        passwordConfirm = inputPasswordConfirm.text;
        Debug.Log("password:"+passwordConfirm);
    }

    // ボタンを押したときにLoginを送信する関数
    public void OnClickLoginButton()
    {
      mode = "login";
      userName = inputUserName.text;
      password = inputPassword.text;

      Debug.Log("clickUsername:"+userName);
      Debug.Log("clickpassword:"+password);
      // userName="test01"; 
      // password="password01";
      // パスワードをハッシュ化する
      // todo
      // 送信する
      StartCoroutine(GetUserRequest(rootUrl,mode, userName, password));
      
      // // 送信後に内部値を空にする
      // userName = "";
      // inputUserName.text = "";
      // password = "";
      // inputPassword.text = "";
      Debug.Log("Button Clicked GetRequest!");

    }
 // ボタンを押したときにユーザーを作成するための送信をする関数
    public void OnClickCreateButton()
    {
      mode = "create";
      userName = inputUserName.text;
      password = inputPassword.text;
      passwordConfirm = inputPasswordConfirm.text;
      Debug.Log(userName);
      Debug.Log(password);
      Debug.Log(passwordConfirm);

      // パスワードを確認する
      if (password != passwordConfirm)
      {
        Debug.Log("パスワードが一致しません");
        password = "";
        passwordConfirm = "";
        // 入力フォームのテキストを空にする
        inputPassword.text = "";
        inputPasswordConfirm.text = "";

        return;
      }else{
        Debug.Log("パスワードが一致しました");
        // パスワードをハッシュ化する
        // todo
        // 送信する
        StartCoroutine(GetUserRequest(rootUrl,mode, userName, password));
      }
      
      // 送信後に内部値を空にする
      userName = "";
      inputUserName.text = "";
      password = "";
      inputPassword.text = "";
      Debug.Log("Button Clicked GetRequest!");

    }

    // JSONを確認するための関数
    public void cheackJson(UnityWebRequest request)
    {
     // レスポンスからJSONデータを取得
        string responseText = request.downloadHandler.text;
        Debug.Log("responseData: " + responseText);

        // statusCodeを取得
        var statusCodeMatch = Regex.Match(responseText, "\"statusCode\"\\s*:\\s*(\\d+)");
        if (statusCodeMatch.Success)
        {
            Debug.Log("statusCode: " + statusCodeMatch.Groups[1].Value);
            SceneManager.LoadScene("menyu");
        }

        // todo

        // bodyの中のuser_idを取得
        var bodyMatch = Regex.Match(responseText, "\"body\"\\s*:\\s*\"(.*?)\"");
        if (bodyMatch.Success)
        {
            var bodyJson = bodyMatch.Groups[1].Value;
            bodyJson = bodyJson.Replace("\\\"", "\""); // エスケープされた引用符を置換

            Debug.Log("bodyJson: " + bodyJson); // デバッグ用にbodyJsonを出力
            
            // todo
            // user_idを取得出来ていない

            // user_idを取得
            var userIdMatch = Regex.Match(bodyJson, "\"user_id\"\\s*:\\s*\"([^\"]+)\"");
            if (userIdMatch.Success)
            {
                Debug.Log("user_id: " + userIdMatch.Groups[1].Value);
            }
            else
            {
                Debug.LogError("user_id not found in bodyJson");
            }
        }
        else
        {
            Debug.LogError("body not found in responseJson");
        }
    }


}

