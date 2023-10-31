Feature: Banking System API Tests

Scenario: Deposit money into a bank account
    Given I have an account number '12345678'
    When I deposit the amount 500 into that account
    Then that accounts new balance should be 1500

Scenario: Withdraw money from a bank account
    Given I have an account number '12345678'
    When I withdraw the amount 500 into that account
    Then that accounts new balance should be 500