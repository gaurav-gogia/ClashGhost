package main

import (
	"fmt"
	"log"
	"net/http"
	"time"

	mgo "gopkg.in/mgo.v2"
	"gopkg.in/mgo.v2/bson"

	"encoding/json"

	"github.com/gorilla/context"
)

type whoAreYou struct {
	UID      string `bson:"uid"`
	Password string `bson:"password"`
	Name     string `bson:"name"`
	Email    string `bson:"email"`
	Sex      string `bson:"sex"`

	TimeStamp string `bson:"timestamp"`
}

const conString = "mongodb://localhost/"

func main() {
	http.HandleFunc("/reg", register)
	http.HandleFunc("/login", login)
	http.HandleFunc("/update", update)
	http.HandleFunc("/passChange", passChange)
	http.HandleFunc("/getuser", getuser)

	fmt.Println("Server running at port 80")
	http.ListenAndServe(":80", context.ClearHandler(http.DefaultServeMux))
}

func register(w http.ResponseWriter, r *http.Request) {
	var person whoAreYou
	result := whoAreYou{}
	var response string

	if r.Method == http.MethodPost {

		sesson, err := mgo.Dial(conString)

		if err != nil {
			log.Println(err)
		}
		defer sesson.Close()

		c := sesson.DB("ClashGhost").C("GhostLayer")
		c1 := sesson.DB("ClashGhost").C("ServeLayer")

		person.Name = r.FormValue("name")
		person.Sex = r.FormValue("sex")
		person.Email = r.FormValue("email")
		person.UID = r.FormValue("user")
		person.TimeStamp = time.Now().Format(time.RFC850)
		pass := r.FormValue("pass")
		person.Password = encryptThePassword(pass)

		err = c1.Find(bson.M{"uid": person.UID}).One(&result)
		err1 := c1.Find(bson.M{"email": person.Email}).One(&result)

		if err == nil || err1 == nil {
			response = "User already exists"
		} else {
			err = c.Insert(person)
			err1 = c1.Insert(person)

			if err != nil || err1 != nil {
				response = "Failed :("
				log.Println(err)
				log.Println(err1)
			} else {
				response = "Success"
			}
		}

		fmt.Fprintf(w, `{"response":"%s"}`, response)
	}

	log.Println(r.URL.Path)
}
func login(w http.ResponseWriter, r *http.Request) {
	var response string
	var person whoAreYou
	result := whoAreYou{}
	if r.Method == http.MethodPost {

		sesson, err := mgo.Dial(conString)

		if err != nil {
			log.Println(err)
		}
		defer sesson.Close()
		c := sesson.DB("ClashGhost").C("ServeLayer")
		person.UID = r.FormValue("user")
		pass := r.FormValue("pass")
		person.Password = encryptThePassword(pass)

		err = c.Find(bson.M{"uid": person.UID, "password": person.Password}).One(&result)
		if err != nil {
			response = "Failed :("
		} else {
			response = result.Name
		}

		fmt.Fprintf(w, `{
		"response":"%s"
	}`, response)

		log.Println(r.URL.Path)
	}
}
func update(w http.ResponseWriter, r *http.Request) {
	var person whoAreYou
	result := whoAreYou{}
	var response string

	if r.Method == http.MethodPost {

		sesson, err := mgo.Dial(conString)

		if err != nil {
			log.Println(err)
		}
		defer sesson.Close()

		c := sesson.DB("ClashGhost").C("GhostLayer")
		c1 := sesson.DB("ClashGhost").C("ServeLayer")

		person.Name = r.FormValue("name")
		person.Sex = r.FormValue("sex")
		person.Email = r.FormValue("email")
		person.UID = r.FormValue("user")
		person.TimeStamp = time.Now().Format(time.RFC850)
		pass := r.FormValue("pass")
		person.Password = encryptThePassword(pass)

		err = c1.Find(bson.M{"uid": person.UID}).One(&result)

		if err != nil {
			response = "User does not exists"
		} else {
			colQuerier := bson.M{"uid": person.UID}
			err = c1.Update(colQuerier, person)
			err1 := c.Insert(person)

			if err != nil || err1 != nil {
				response = "Failed :("
				log.Println(err)
				log.Println(err1)
			} else {
				response = "Success"
			}
		}

		fmt.Fprintf(w, `{"response":"%s"}`, response)
	}

	log.Println(r.URL.Path)
}
func passChange(w http.ResponseWriter, r *http.Request) {}
func getuser(w http.ResponseWriter, r *http.Request) {
	var person whoAreYou
	result := whoAreYou{}
	var response string

	if r.Method == http.MethodPost {

		sesson, err := mgo.Dial(conString)

		if err != nil {
			log.Println(err)
		}
		defer sesson.Close()
		c := sesson.DB("ClashGhost").C("ServeLayer")

		person.UID = r.FormValue("user")

		err = c.Find(bson.M{"uid": person.UID}).One(&result)
		if err != nil {
			response = "Failed :("
			fmt.Fprintf(w, `{"userIsHere":"%s"}`, response)
		} else {
			resp, _ := json.MarshalIndent(result, "", " ")
			fmt.Fprintf(w, string(resp))
		}
	}

	log.Println(r.URL.Path)
}

// PasswordCryptor
func convertToBits(n, pad int) string {
	var result string

	for ; n > 0; n = n / 2 {
		if n%2 == 0 {
			result = "1" + result
		} else if n%3 == 0 {
			result = "1" + result
		} else {
			result = "0" + result
		}
	}

	for i := len(result); i < pad; i++ {
		result = "0" + result
	}

	return result
}

func encryptThePassword(str string) string {
	var result string

	data := []rune(str)

	for _, i := range data {
		result = convertToBits(int(i), 8) + result
	}

	return result
}
