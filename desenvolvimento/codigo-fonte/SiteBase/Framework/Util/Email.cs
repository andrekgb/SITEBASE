using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// E-mail.
    /// </summary>
    public class Email
    {
        #region "Construtores"

        /// <summary>
        /// Construtora vazio.
        /// </summary>
        /// <remarks></remarks>
        public Email()
        {
            this.Sender_ = string.Empty;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="pFrom"></param>
        /// <param name="pTo"></param>
        /// <param name="pTitulo"></param>
        /// <param name="pTexto"></param>
        /// <remarks></remarks>
        public Email(string pFrom, string pTo, string pTitulo, string pTexto)
            : this(pFrom, pTo, pTitulo, pTexto, string.Empty)
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="pFrom"></param>
        /// <param name="pTo"></param>
        /// <param name="pTitulo"></param>
        /// <param name="pTexto"></param>
        /// <remarks></remarks>
        public Email(string pFrom, string pTo, string pTitulo, string pTexto, string pSender)
        {
            this.From_ = pFrom;
            this.To_ = pTo;
            this.Titulo_ = pTitulo;
            this.Texto_ = pTexto;
            this.Sender_ = pSender;
        }

        #endregion

        #region "Propriedades"

        private String Sender_;
        private String From_;
        private String FromName_;
        private String To_;
        private MailAddressCollection Cc_;
        private String Titulo_;
        private String Texto_;
        public int PortNumber { get; set; }
        public String ReplyTO_ { get; set; }

        #endregion

        #region "Atributos"

        /// <summary>
        /// Email From.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailFrom
        {
            get { return this.From_; }
            set { this.From_ = value; }
        }

        /// <summary>
        /// From Name.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailFromName
        {
            get { return this.FromName_; }
            set { this.FromName_ = value; }
        }

        /// <summary>
        /// Email To.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailTo
        {
            get { return this.To_; }
            set { this.To_ = value; }
        }

        /// <summary>
        /// Email Cc (Copia).
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public MailAddressCollection EmailCc
        {
            get { return this.Cc_; }
            set { this.Cc_ = value; }

        }

        /// <summary>
        /// Email Titulo.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailTitulo
        {
            get { return this.Titulo_; }
            set { this.Titulo_ = value; }
        }

        /// <summary>
        /// Email Texto.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailTexto
        {
            get { return this.Texto_; }
            set { this.Texto_ = value; }
        }

        /// <summary>
        /// Email Sender.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public String EmailSender
        {
            get { return this.Sender_; }
            set { this.Sender_ = value; }
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Envia o e-mail.
        /// </summary>
        /// <remarks></remarks>
        public void Enviar()
        {
            //Smtp
            //SmtpClient oSmtp = new SmtpClient();
            //oSmtp.Port = 587;

            //oSmtp.EnableSsl = false;
            //oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Se estiver setado uma porta diferente
            //if (this.PortNumber > 0)
            //{
             //   oSmtp.Port = this.PortNumber;
            //}

            this.From_ = "florestas@florestasdofuturo.org.br";
            this.Sender_ = "florestas@florestasdofuturo.org.br";

            MailMessage oEmail = new MailMessage();

            //e-mail de envio
            if (!this.Sender_.Equals(string.Empty))
            {
                oEmail.Sender = new MailAddress(this.Sender_);
            }

            /// Reply to
            if (string.IsNullOrEmpty(this.ReplyTO_) == false)
            {
                oEmail.ReplyTo = new MailAddress(this.ReplyTO_);
            }

            //remetente do email
            if ((FromName_ != null) && FromName_.Length > 0)
            {
                oEmail.From = new MailAddress(this.From_, this.FromName_);
            }
            else
            {
                oEmail.From = new MailAddress(this.From_, "Florestas do Futuro");
            }

            //destinatario do email
            oEmail.To.Add(this.To_);

            //copia
            if (this.EmailCc != null)
            {
                for (int i = 0; i < this.EmailCc.Count; i++)
                {
                    oEmail.CC.Add(this.EmailCc[i]);
                }
            }

            //prioridade de envio
            oEmail.Priority = MailPriority.Normal;

            //define o formato do email
            oEmail.IsBodyHtml = true;

            //define o assunto do email
            oEmail.Subject = this.Titulo_;

            //define a mensagem principal do email
            oEmail.Body = this.Texto_;

            //Para evitar problemas com caracteres especiais configuramos o Charset
            oEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            oEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            System.Net.Mail.SmtpClient oSmtp = new System.Net.Mail.SmtpClient();
            //Alocamos o endereço do host para enviar os e-mails 
            oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            oSmtp.Credentials = new System.Net.NetworkCredential("florestas@florestasdofuturo.org.br", "C1a2P9aN7Hw23");
            oSmtp.Host = "smtp.florestasdofuturo.org.br";
            oSmtp.Port = 587;

            try
            {
                //Dispara
                oSmtp.Send(oEmail);
            }
            finally
            {
                //Libera recursos
                oEmail.Dispose();
            }
        }

        #endregion

    }
}