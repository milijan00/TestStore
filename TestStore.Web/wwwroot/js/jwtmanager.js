
const token = {
    content() {
        try {
            let strToken = document.cookie.split(";").find((row) => row.startsWith("access="))?.split("=")[1];
            let payload = strToken.split(".")[1];
            if (payload) {
                let json = JSON.parse(atob(payload));
                return json;
            } else return null;
        } catch (e) {
            return null;
        }
    }
}

export default token;