//=================================================================================== 
// INSTITUTO INFNET - GRADUAÇÃO EM ANÁLISE E DESENVOLVIMENTO DE SISTEMAS
// TRABALHO DE CONCLUSÃO DO CURSO
// AUTORES:
// JAIR MARTINS
// LEVY FIALHO
// MARCELO SÁ
//===================================================================================
// Este código foi desenvolvido com o objetivo de demonstrar a aplicação prática de 
// padrões de desenvolvimento de software adotados no mercado no ano de 2012.

// Mais especificamente, o código demonstra a aplicação prática de conceitos abordados
// em Domain driven Design e Patterns of Application Architechture na plataforma .Net
//===================================================================================

using System;
using System.Text;
using System.Security.Cryptography;

namespace SmsGateway.Domain.Seedwork
{
    public static class Encryption
    {
        private const string Key = "FC9109AB819E459A910C31F0FC0AE4E9";

        public static string ToBase64(string text)
        {

            return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(text));
        }

        public static string FromBase64(string texto)
        {
            return System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(texto));
        }

        public static string FromMd5(string text)
        {
            var des = new TripleDESCryptoServiceProvider();
            var hashmd5 = new MD5CryptoServiceProvider();
            des.Key = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(Key));
            des.Mode = CipherMode.ECB;
            var desdencrypt = des.CreateDecryptor();
            byte[] buff = Convert.FromBase64String(text);
            return Encoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        public static string ToMd5(string text)
        {
            var des = new TripleDESCryptoServiceProvider();
            var hashmd5 = new MD5CryptoServiceProvider();
            des.Key = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(Key));
            des.Mode = CipherMode.ECB;
            var desdencrypt = des.CreateEncryptor();
            byte[] buff = Encoding.ASCII.GetBytes(text);
            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

        }
    }
}

