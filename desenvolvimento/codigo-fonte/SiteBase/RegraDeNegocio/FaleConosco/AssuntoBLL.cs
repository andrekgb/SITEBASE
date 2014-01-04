using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Linq;

namespace RegraDeNegocio.FaleConosco
{
    public class AssuntoBLL : BaseBLL
    {
        #region Métodos

        /// <summary>
        /// Retorna A listagem de assuntos
        /// </summary>
        /// <param name="orderByColumnName"></param>
        /// <param name="orderByDescending"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<Fale_Conosco_Assunto> obter(string orderByColumnName, bool orderByDescending, int skip, int take)
        {
            var query = from p in this.ctx.Fale_Conosco_Assuntos select p;

            if (!string.IsNullOrEmpty(orderByColumnName))
                query = query.OrderBy(orderByColumnName, orderByDescending);
            else
                query = query.OrderBy("Nome", true);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return new List<Fale_Conosco_Assunto>(query);
        }

        /// <summary>
        /// Conta o total de registros
        /// </summary>
        /// <returns></returns>
        public int obterTotalRowCount()
        {
            var query = from p in this.ctx.Fale_Conosco_Assuntos select p;
            return query.Count();
        }

        /// <summary>
        /// Retorna assunto por ID
        /// </summary>
        /// <param name="assuntoID"></param>
        /// <returns></returns>
        public Fale_Conosco_Assunto obter(int assuntoID)
        {
            return (from p in this.ctx.Fale_Conosco_Assuntos where p.AssuntoID == assuntoID select p).FirstOrDefault();
        }

        /// <summary>
        /// Salva um registro
        /// </summary>
        /// <param name="assunto"></param>
        public void salvar(Fale_Conosco_Assunto assunto)
        {
            this.validar(assunto, false);

            try
            {
                // salva
                this.ctx.Fale_Conosco_Assuntos.InsertOnSubmit(assunto);
                this.ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// Altera o nome do assunto
        /// </summary>
        /// <param name="assunto"></param>
        public void alterar(Fale_Conosco_Assunto assunto)
        {
            this.validar(assunto, true);

            try
            {
                // Excluir os e-mails antes de vincular novamente
                ctx.ExecuteCommand("DELETE FROM Fale_Conosco_Emails WHERE AssuntoID={0}", assunto.AssuntoID.ToString());

                // registro do banco
                Fale_Conosco_Assunto reg = (from p in this.ctx.Fale_Conosco_Assuntos
                                            where p.AssuntoID == assunto.AssuntoID
                                            select p).FirstOrDefault();
                reg.Fale_Conosco_Emails = assunto.Fale_Conosco_Emails;

                reg.Nome = assunto.Nome;
                reg.IsAtivo = assunto.IsAtivo;
                this.ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// Exclui um assunto. Alé do ID é verificado o idioma
        /// </summary>
        /// <param name="AssuntoID"></param>
        public void excluir(int assuntoID)
        {
            try
            {
                // Excluir os e-mails 
                ctx.ExecuteCommand("DELETE FROM Fale_Conosco_Emails WHERE AssuntoID={0}", assuntoID.ToString());

                Fale_Conosco_Assunto reg = (from p in this.ctx.Fale_Conosco_Assuntos
                                            where p.AssuntoID == assuntoID
                                            select p).FirstOrDefault();

                this.ctx.Fale_Conosco_Assuntos.DeleteOnSubmit(reg);
                this.ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw ex;
            }

        }

        /// <summary>
        /// Retorna a lista de e-mails vinculados a um assunto
        /// </summary>
        /// <param name="assuntoID"></param>
        /// <returns></returns>
        public List<EmailDTO> obterEmails(int assuntoID)
        {
            var query = (from p in this.ctx.Fale_Conosco_Emails where p.AssuntoID == assuntoID select p).OrderBy(p=> p.Nome);

            if (query == null)
            {
                return null;
            }
            else
            {
                List<EmailDTO> lista = new List<EmailDTO>();

                foreach (var item in query)
                {
                    lista.Add(new EmailDTO { Nome = item.Nome, Email = item.Email});
                }

                return lista;
            }
        }

        /// <summary>
        /// Ativar / desativar assunto
        /// </summary>
        /// <param name="assuntoID"></param>
        /// <param name="isAtivo"></param>
        public void ativar(int assuntoID, bool isAtivo)
        {
            Fale_Conosco_Assunto reg = (from p in this.ctx.Fale_Conosco_Assuntos where p.AssuntoID == assuntoID select p).FirstOrDefault();

            if (reg == null)
                throw new Framework.Erros.Excecao("Assunto n~ao enoctrado");

            reg.IsAtivo = isAtivo;
            this.ctx.SubmitChanges();
        }

        #endregion

        #region "Validação"

        /// <summary>
        /// Valida o assunto
        /// </summary>
        /// <param name="assunto"></param>
        private void validar(Fale_Conosco_Assunto assunto, bool isAlteracao)
        {
            if (assunto == null)
                throw new Framework.Erros.Excecao("Os dados do assuntos não foram encontrados");

            if (assunto.Fale_Conosco_Emails == null || assunto.Fale_Conosco_Emails.Count == 0)
                throw new Framework.Erros.Excecao("Não foram adicionados e-mails para receber os formulários enviados para esse assunto.");

            // Se existe outro assunto com o nome nome preenchido
            var hasMesmoNome = (from p in this.ctx.Fale_Conosco_Assuntos where p.Nome == assunto.Nome && p.AssuntoID != assunto.AssuntoID select p.AssuntoID).FirstOrDefault();
            if (hasMesmoNome > 0)
                throw new Framework.Erros.Excecao("Já existe outro assunto cadastrado com este mesmo nome");
        }

        #endregion
    }
}
