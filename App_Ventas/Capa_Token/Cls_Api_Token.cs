using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Capa_Entidad.Base; 
namespace Capa_Token
{
    public class Cls_Api_Token
    {
        private static string key_secret = "MN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR";
        private static string KEY_ID_USUARIO = "ID_USUARIO";
        private static string KEY_COD_USUARIO = "COD_USUARIO";
        private static string KEY_RECUERDAME = "RECUERDAME";
        private static string KEY_ID_SUCURSAL = "ID_SUCURSAL";

        public static string Generar(string ID_USUARIO, string COD_USUARIO, string RememberMe, string ID_SUCURSAL)
        {
            byte[] key = Convert.FromBase64String(key_secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[] { 
                    new Claim(type: KEY_ID_USUARIO, value: ID_USUARIO),
                    new Claim(type: KEY_COD_USUARIO, value: COD_USUARIO),
                    new Claim(type: KEY_RECUERDAME, value: RememberMe), 
                    new Claim(type: KEY_ID_SUCURSAL, value: ID_SUCURSAL), 
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static string Generar(string ID_USUARIO)
        {
            byte[] key = Convert.FromBase64String(key_secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[] { new Claim(type: KEY_ID_USUARIO, value: ID_USUARIO) }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal Principal(string Token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadJwtToken(Token);

                if (securityToken == null)
                {
                    return null;
                }
                byte[] key = Convert.FromBase64String(key_secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken security;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(Token, parameters, out security);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static bool Validar_Token(string TOKEN)
        {
            bool valido = false;
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_ID_USUARIO)
                        {
                            valido = true;
                            break;
                        }
                    }
            }
            catch
            {
                valido = false;
            }
            return valido;
        }

        public static int Claim_ID_USUARIO(string TOKEN)
        {
            int ID_USUARIO = 0;
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_ID_USUARIO)
                        {
                            ID_USUARIO = int.Parse(claim.Value);
                            break;
                        }
                    }
            }
            catch
            {
                ID_USUARIO = 0;
            }
            return ID_USUARIO;
        }

        public static string Claim_COD_USUARIO(string TOKEN)
        {
            string COD_USUARIO = "";
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_COD_USUARIO)
                        {
                            COD_USUARIO = claim.Value;
                            break;
                        }
                    }
            }
            catch
            {
                COD_USUARIO = "";
            }
            return COD_USUARIO;
        }

        public static int Claim_RECUERDAME(string TOKEN)
        {
            int RECUERDAME = 0;
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_RECUERDAME)
                        {
                            RECUERDAME = int.Parse(claim.Value);
                            break;
                        }
                    }
            }
            catch
            {
                RECUERDAME = 0;
            }
            return RECUERDAME;
        }

        public static int Claim_ID_SUCURSAL(string TOKEN)
        {
            int ID_SUCURSAL = 0;
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_ID_SUCURSAL)
                        {
                            ID_SUCURSAL = int.Parse(claim.Value);
                            break;
                        }
                    }
            }
            catch
            {
                ID_SUCURSAL = 0;
            }
            return ID_SUCURSAL;
        }

        public static Cls_Ent_SetUpLogin SetUpLogin(string TOKEN)
        {
            Cls_Ent_SetUpLogin setUp = new Cls_Ent_SetUpLogin();
            try
            {
                var Claim = Cls_Api_Token.Principal(TOKEN);
                if (Claim != null)
                    foreach (Claim claim in Claim.Claims)
                    {
                        if (claim.Type == KEY_ID_USUARIO)
                        {
                            setUp.ID_USUARIO = int.Parse(claim.Value);
                        } else if (claim.Type == KEY_COD_USUARIO)
                        {
                            setUp.COD_USUARIO = claim.Value;
                        } else if (claim.Type == KEY_RECUERDAME)
                        {
                            setUp.RECUERDAME = int.Parse(claim.Value);
                        }
                        else if (claim.Type == KEY_ID_SUCURSAL)
                        {
                            setUp.ID_SUCURSAL = int.Parse(claim.Value);
                        }
                    }
            }
            catch
            {
                setUp = new Cls_Ent_SetUpLogin(); 
            }
            return setUp;
        }


    }
}