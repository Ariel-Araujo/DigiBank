using DigiBank.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Contratos
{
    public interface IConta
    {
        void Deposita(double valor);
        bool Saca(double valor);
        double ConsultaSaldo();
        string GetCodigoDobanco();
        string GetNumeroAgencia();
        string GetNumeroConta();
        List<Extrato> Extrato();

    }
}
