# algoriza-internship-241
admin: admin@test.com
password: V44{aaw%/A
doctor-password: [%tw459r

DB link: https://drive.google.com/file/d/1iHylDNjULTQGDHUQXG4fl91SmFH2d1eQ/view?usp=sharing

Features:
- User Register/login
- Admin CRUD doctors
- Admin CRUD coupons
- Doctor CRUD appointments, confirm booking and get all his bookings
- Patient Book, Cancel booking and retrieve his bookings
- Gender/ Days/ Specialization Localization
- Send doctor credentials through email using SendGrid (need to add your API key and email)
- JWT token contains user identifier and role

Documentation notes:
- Central result (success/failure) location and format inside Result<T> with standard error format
- Mapper configuration including member mapping in MapperConfig file
- Appconst DomainModels is used when passing includes to repository methods
- Date seed for roles and specialization in DbContext onModelCreating, and admin seed in a static class (ApplicationDbInitializer) called once when app startup
