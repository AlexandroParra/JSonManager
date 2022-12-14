using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager.SavedHttpRequests
{
    /// <summary>
    /// Se crea esta interface para filtrar las entidades que se pasan al formulario de edición.
    /// </summary>
    public interface IEditableEntity
    {
        EntityType EntityType { get; }
    }
}
