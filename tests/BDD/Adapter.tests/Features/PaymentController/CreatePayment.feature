Feature: PaymentController

  Scenario: Create payment with valid input
    Given a payment request with orderId "o-1" and totalPrice 10 and paymentMethod "Pix"
    When I call CreateAsync
    Then the response should contain a payment id
