namespace TestHotelSmart.WebApi.DTO.Response

{

    public class ResponseQuery<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseQuery()
        {
            this.Successful = true;
        }
        /// <summary>
        /// True: indica que la operación se ejecutó exitósamene.
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// Código de fallo en caso de presentarse un error.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Detalle del error que pueda presentarse.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Entidad compuesta con información 
        /// </summary>
        public T Result { get; set; }

        public void ResponseMessage(string message, bool successfull, string errorMessage = "")
        {
            this.Message = message;
            this.Successful = successfull;
            this.ErrorMessage = errorMessage;
        }
    }
}
