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


namespace SmsGateway.Domain.Seedwork
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Classe base para 'Value Objects' do domínio
    /// </summary>
    /// <typeparam name="TValueObject">Tipo do objeto</typeparam>
    public class ValueObject<TValueObject> :IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {

        #region IEquatable and Override Equals operators

        
        public bool Equals(TValueObject other)
        {            
            if ((object)other == null)
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            //compara todas as propriedades públicas
            PropertyInfo[] publicProperties = this.GetType().GetProperties();

            if ((object)publicProperties != null
                &&
                publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);

                    
                    if (typeof(TValueObject).IsAssignableFrom(left.GetType()))
                    {
                        //checa por self-references
                        return Object.ReferenceEquals(left, right);
                    }
                    else
                        return left.Equals(right);
                    

                });
            }
            else
                return true;
        }
    
        public override bool Equals(object obj)
        {
            if ((object)obj == null)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            var item = obj as ValueObject<TValueObject>;
            
            if ((object)item != null)
                return Equals((TValueObject)item);
            else
                return false;

        }


        public override int GetHashCode()
        {
            int hashCode = 31;
            bool changeMultiplier = false;
            int index = 1;

            //compare todas as propriedades públicas
            PropertyInfo[] publicProperties = this.GetType().GetProperties();

            
            if ((object)publicProperties != null
                &&
                publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    object value = item.GetValue(this, null);

                    if ((object)value != null)
                    {

                        hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index * 13);
                }
            }

            return hashCode;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
            
        }        

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
