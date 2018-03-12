class CheckingAccount {
    constructor(clientId, email, firstName, lastName) {
        [this.clientId, this.email, this.firstName, this.lastName, this.products] =
            [clientId, email, firstName, lastName, []]
    }

    get clientId() {
        return this._clientId
    }

    set clientId(newClient) {
        if (newClient.length === 6) {
            this._clientId = newClient
        }
        else {
            throw new TypeError("Client ID must be a 6-digit number")
        }
    }

    get email() {
        return this._email
    }

    set email(newEmail) {
        let validMailReg = /^[A-Za-z0-9]+@[A-Za-z.]+$/;
        if (!validMailReg.test(newEmail)) {
            throw new TypeError("Invalid e-mail")
        }
        this._email = newEmail;
    }

    get firstName() {
        return this._firstName
    }

    set firstName(newFirstName) {
        let nameReg = /^[A-Za-z]+$/g;
        let nameLenght = /^.{3,20}$/g;
        if (!nameReg.test(newFirstName)) {
            throw new TypeError("First name must contain only Latin characters")
        }
        if (!nameLenght.test(newFirstName)) {
            throw new TypeError("First name must be between 3 and 20 characters long")
        }
        this._firstName = newFirstName;
    }

    get lastName() {
        return this._lastName
    }

    set lastName(newLastName) {
        let nameReg = /^[A-Za-z]+$/g;
        let nameLenght = /^.{3,20}$/g;
        if (!nameReg.test(newLastName)) {
            throw new TypeError("Last name must contain only Latin characters")
        }
        if (!nameLenght.test(newLastName)) {
            throw new TypeError("Last name must be between 3 and 20 characters long")
        }
        this._lastName = newLastName;
    }
}