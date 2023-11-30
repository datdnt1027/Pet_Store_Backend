# Pet Store API

-   [Pet Store API](#pet-store-api)
    -   [Register](#register)
        -   [Register Request](#register-request)
        -   [Register Response](#register-response)
    -   [Login](#Login)
        -   [Login Request](#register-request)
        -   [Login Response](#register-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "Dang Nguyen Thien",
    "lastName": "Dat",
    "email": "20110629@student.hcmute.edu.vn",
    "password": "Test123@!"
}
```

#### Register Response

```json
200 OK
```

```json
{
    "id": "f8894a0d-a926-46aa-b772-02d24b37fdf0",
    "firstName": "Dang Nguyen Thien",
    "lastName": "Dat",
    "email": "20110629@student.hcmute.edu.vn",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmODg5NGEwZC1hOTI2LTQ2YWEtYjc3Mi0wMmQyNGIzN2ZkZjAiLCJnaXZlbl9uYW1lIjpbIkRhbmcgTmd1eWVuIFRoaWVuIiwiRGF0Il0sImp0aSI6ImY4ODk0YTBkLWE5MjYtNDZhYS1iNzcyLTAyZDI0YjM3ZmRmMCIsImV4cCI6MTY5NzM3MTY3MH0.C0DW_vU-zUMCEBZej9zkZwZzmEIf9sh0g6uo0F191Gc"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email": "20110629@student.hcmute.edu.vn",
    "password": "Test123@!"
}
```

#### Login Response

```js
200 OK
```

```json
{
    "id": "f8894a0d-a926-46aa-b772-02d24b37fdf0",
    "firstName": "Dang Nguyen Thien",
    "lastName": "Dat",
    "email": "20110629@student.hcmute.edu.vn",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmODg5NGEwZC1hOTI2LTQ2YWEtYjc3Mi0wMmQyNGIzN2ZkZjAiLCJnaXZlbl9uYW1lIjpbIkRhbmcgTmd1eWVuIFRoaWVuIiwiRGF0Il0sImp0aSI6ImY4ODk0YTBkLWE5MjYtNDZhYS1iNzcyLTAyZDI0YjM3ZmRmMCIsImV4cCI6MTY5NzM3MTY3MH0.C0DW_vU-zUMCEBZej9zkZwZzmEIf9sh0g6uo0F191Gc"
}
```
