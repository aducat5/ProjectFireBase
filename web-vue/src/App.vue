<script setup lang="ts">
  import { ref } from 'vue'
  import { initializeApp } from "firebase/app";
  import {
    getAuth,
    signInWithPopup,
    GoogleAuthProvider,
    connectAuthEmulator,
  } from "firebase/auth";

  const cityName = ref('')
  const stateName = ref('')
  const username = ref('');
  const password = ref('');

  const authToken = ref('');

  const firebaseConfig = {
    apiKey: "AIzaSyDQS1L2PkMJTzkp6JKaoodS4ypA3GHAhIc",
    authDomain: "fir-denemesi-7ffbe.firebaseapp.com",
    databaseURL: "https://fir-denemesi-7ffbe.firebaseio.com",
    projectId: "fir-denemesi-7ffbe",
    storageBucket: "fir-denemesi-7ffbe.appspot.com",
    messagingSenderId: "440601479904",
    appId: "1:440601479904:web:b9377db1a671425fe39e85"
  };

  const app = initializeApp(firebaseConfig);
  const provider = new GoogleAuthProvider();
  const auth = getAuth(app);

  connectAuthEmulator(auth, 'http://127.0.0.1:9099');

  const createCity = async () => {
    const name = cityName.value
    const state = stateName.value
    const url = 'https://localhost:7233/city/add'
    
    const response = await fetch(url, {
      method: "POST",
      headers:  { 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + authToken.value
      },
      body: JSON.stringify({name, state})
    });
    console.log(response);
  }

  const firebaseLogin = () => {
    signInWithPopup(auth, provider)
      .then(async (result) => {
        const credential = GoogleAuthProvider.credentialFromResult(result);
        const token = credential!.accessToken;
        const user = result.user;
        authToken.value = await user.getIdToken();
        console.log(`Auth token: ${authToken.value}`);
      })
      .catch((error) => {
        console.log(error)
      });
  };
</script>

<template>
  <div>
    Google Login <br />
    <button @click="firebaseLogin">Login</button>
  </div>
  <p>{{ authToken === "" ? "Click Login" : authToken }}</p>

  <hr />
  Username
  <input type="text" v-model="username" />
  Password
  <input type="password" v-model="password" />
  <hr />

  <div>
    Name
    <input type="text" v-model="cityName" />
    <br />

    State
    <input type="text" v-model="stateName" />
    <br />
    <button @click="createCity">Add</button>
  </div>
</template>

<style scoped>

  p {
    word-break: break-all;
  }
</style>
