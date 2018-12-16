# tya


#### .NetCore 2.1 App for simple ShoppingCart 

    - Has Simple implementation of campaigns and coupons
        - Campaings are specific discounts based on categories
        - Multiple campaigns can be defined on a category, system will find the best one(for the customer) to apply on the cart item
        - If a product's main category does not have a campaign defined, system looks for a campaign that has been defined on a parent category of product(it choses the first it can find, if it cant find any(not even on highest category) no campaigns are applied on the cart item)

    - Sample commands can be found within app if you type 'pleaseHelp'

    - Only one cart at a time, but n number of product and campaigns can be defined.

    - Only one coupon can be applied on the cart and it is applied at the moment it is created

    - ShoppingCart service is responsible for any operation on the cart

    - Campaign, product and category services are responsible for their appropriate components

    - Titles are unique for campaigns and products

    - Categories can be created with or without a parent category
        - But you can not create a category if you enter a non-existent category as a parent category

    - You can not create a campaign if you enter a non-existent category as  category
    
    - You can not create a product if you enter a non-existent category as  category

    - You can not create a category if you enter a non-existent category as parent category
