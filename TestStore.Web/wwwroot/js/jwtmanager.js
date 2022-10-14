
const token = {
    content() {
        try {
            let strToken = localStorage.getItem("access");
            let payload = strToken.split(".")[1];
            if (payload) {
                let json = JSON.parse(atob(payload));
                return json;
            } else return null;
        } catch (e) {
            return null;
        }
    },
    isValid() {
        return this.content();
    }
}

export default token;