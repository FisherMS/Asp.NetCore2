 namespace  JwtAuthSample
 {
     public class JwtSettings{

//颁发者
         public string Issuer{get;set;}

//给哪些客户端用
         public string  Audience{get;set;}


    public string SecretKey{get;set;}

     }

 }