using DAO;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controllers
{
    public class UsuarioController : Controller
    {
        AcessoBancoDados bd = new AcessoBancoDados();
        
        public ActionResult Index()
        {
            return View(SelecionaTodosUsuarios());
        }

        public ActionResult Detalhes(int Id = 0)
        {
            return View(SelecionaUsuario(Id));
        }
        
        public ActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inserir(UsuarioModel _usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string nome = _usuarioModel.Nome.Replace("'", "''");
                    
                    bd.Conectar();
                    string comando = "INSERT INTO usuario(nome, email, telefone, senha) VALUES ('" + nome + "','" + _usuarioModel.Email + "','" + _usuarioModel.Telefone + "','" + _usuarioModel.Senha + "')";
                    bd.ExecutarComandoSQL(comando);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar inserir: " + ex.Message);
            }
            finally
            {
                bd = null;
            }

            return View();
        }

        public ActionResult Atualizar(int Id = 0)
        {
            return View(SelecionaUsuario(Id));
        }

        [HttpPost]
        public ActionResult Atualizar(UsuarioModel _usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string nome = _usuarioModel.Nome.Replace("'", "''");

                    bd.Conectar();
                    string comando = "UPDATE usuario SET nome = '" + nome + "', email = '" + _usuarioModel.Email + "', telefone = '" + _usuarioModel.Telefone + "', senha = '" + _usuarioModel.Senha + "' WHERE id = " + _usuarioModel.Id;
                    bd.ExecutarComandoSQL(comando);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar: " + ex.Message);
            }
            finally
            {
                bd = null;
            }

            return View();
        }

        public ActionResult Excluir(int Id = 0)
        {
            return View(SelecionaUsuario(Id));
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmed(UsuarioModel _usuarioModel)
        {
            try
            {
                bd.Conectar();
                string comando = "DELETE FROM usuario WHERE id = " + _usuarioModel.Id;
                bd.ExecutarComandoSQL(comando);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar excluir: " + ex.Message);
            }
            finally
            {
                bd = null;
            }

            return RedirectToAction("Index");
        }

        public List<UsuarioModel> SelecionaTodosUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            try
            {
                bd.Conectar();
                var dr = bd.RetDataReader("SELECT id, nome, email, telefone, senha FROM usuario");

                while (dr.Read())
                {
                    usuarios.Add(new UsuarioModel()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nome = dr["Nome"].ToString(),
                        Email = dr["Email"].ToString(),
                        Telefone = dr["Telefone"].ToString(),
                        Senha = dr["Senha"].ToString(),
                    });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar selecionar: " + ex.Message);
            }
            finally
            {
                bd = null;
            }

            return usuarios;
        }

        public UsuarioModel SelecionaUsuario(int Id)
        {
            UsuarioModel usuario = new UsuarioModel();

            try
            {
                bd.Conectar();
                var dr = bd.RetDataReader("SELECT id, nome, email, telefone, senha FROM usuario WHERE id=" + Id);

                while (dr.Read())
                {
                    usuario.Id = Convert.ToInt32(dr["Id"]);
                    usuario.Nome = dr["Nome"].ToString();
                    usuario.Email = dr["Email"].ToString();
                    usuario.Telefone = dr["Telefone"].ToString();
                    usuario.Senha = dr["Senha"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar selecionar: " + ex.Message);
            }
            finally
            {
                bd = null;
            }

            return usuario;
        }

        public bool VerificaUsuario(UsuarioModel usuario)
        {
            bool dw;

            try
            {
                bd.Conectar();
                dw = bd.RetDataRow("SELECT nome, senha FROM usuario WHERE nome='" + usuario.Nome + "' AND senha='" + usuario.Senha + "'");

                return dw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar selecionar: " + ex.Message);
            }
            finally
            {
                bd = null;
            }
        }

    }
}
