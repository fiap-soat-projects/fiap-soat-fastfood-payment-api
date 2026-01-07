using Business.Entities;
using Business.Exceptions;
using Business.Entities.Exceptions;
using Business.Entities.Enums;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class PaymentResultValidationTests
{
    private PaymentResult CreateBase() => new PaymentResult
    {
        Id = "id",
        PaymentMethod = PaymentMethod.Pix.ToString(),
        PaymentStatus = PaymentStatus.Pending.ToString(),
        QrCode = "qr",
        QrCodeBase64 = "b64",
        Amount = 1,
        PaymentResponse = "resp"
    };

    [Fact]
    public void Setting_Id_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.Id = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.Id = "   ");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.Id = null!);
    }

    [Fact]
    public void Setting_PaymentMethod_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentMethod = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentMethod = null!);
    }

    [Fact]
    public void Setting_PaymentStatus_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentStatus = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentStatus = null!);
    }

    [Fact]
    public void Setting_QrCode_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.QrCode = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.QrCode = null!);
    }

    [Fact]
    public void Setting_QrCodeBase64_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.QrCodeBase64 = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.QrCodeBase64 = null!);
    }

    [Fact]
    public void Setting_Amount_To_ZeroOrLess_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.Amount = 0);
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.Amount = -5);
    }

    [Fact]
    public void Setting_PaymentResponse_To_NullOrWhitespace_Throws_InvalidEntityPropertyException()
    {
        var pr = CreateBase();

        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentResponse = "");
        Assert.Throws<InvalidEntityPropertyException<PaymentResult>>(() => pr.PaymentResponse = null!);
    }
}
