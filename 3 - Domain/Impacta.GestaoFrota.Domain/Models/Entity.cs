using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Impacta.GestaoFrota.Domain.Models
{
    //Super classe mãe
    //Só serve para ser herdada não instanciada
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new ValidationResult("");
        }

        public Guid Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; }


        public void AdicionarErrosValidação(ValidationResult validationResult)
        {
            //ValidationResult.Add(validationResult);
        }
        public void ZerarListaErros()
        {
            //ValidationResult = new ValidationResult();
        }

        public abstract bool EhValido();

    }
}
