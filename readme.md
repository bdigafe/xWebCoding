### Purpose

This is a sample app built to show how a subscription invoice can be generated without applying a payment using xWeb.

### Sandbox use only

Do not point to a production xWeb URL.

### Usage

- Load the solution in Visual Studio.
- Update the web service reference.
- Update the app.config file with a valid xweb URL, username, and password.
- Modify the main code to change the customer key used for the transaction.
- After setting up the sample, run the project.
- If all goes right, the program will display the created invoice code.

### Setup

- Setup one or more subscription products. The product must be set with Sell Online Flag.
- You can update the program.cs with a specific product or let the program choose a subscription product.
- Setup a price and flag the pricess with show online.
- Make sure the price is available for the customer (check price attributes).

### Verification

- Load the customer profile and check a new invoice is created.
- The invoice is a proforma and there is **payment**
- There is a subscription record under the customer record, but it is not yet active until it is paid.
- You can apply payment on iWeb, and verify the subscription terms are updated accordingly.
