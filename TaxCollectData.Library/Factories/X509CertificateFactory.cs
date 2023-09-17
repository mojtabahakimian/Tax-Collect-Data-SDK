using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace TaxCollectData.Library.Factories;

public class X509CertificateFactory
{
    public X509Certificate ReadCertificateFromFile(string certificatePath)
    {
        var pemReader = new PemReader(new StreamReader(certificatePath));
        return DotNetUtilities.ToX509Certificate((Org.BouncyCastle.X509.X509Certificate)pemReader.ReadObject());
    }
}