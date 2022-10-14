
const token = {
    content() {
        let strToken = localStorage.getItem("access");
        let payload = strToken.split(".")[1];
        if (payload) {
            let json = JSON.parse(atob(payload));
            return json;
        } else return null;
    }
}

export default token;