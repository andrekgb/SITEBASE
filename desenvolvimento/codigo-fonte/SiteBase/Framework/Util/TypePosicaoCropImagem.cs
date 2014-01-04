using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Posição de crop da imagem.
    /// </summary>
    public enum TypePosicaoCropImagem : int
    {
        LefTop = 0,
        CenterTop = 1,
        RightTop = 2,
        LeftMiddle = 3,
        RightMiddle = 4,
        LeftBottom = 5,
        CenterBottom = 6,
        RightBottom = 7,
        CenterMiddle = 8
    }
}
