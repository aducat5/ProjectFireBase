<script setup lang="ts">
  import { ref } from 'vue'
  const cityName = ref('')
  const stateName = ref('')

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

  const createCity = async () => {
    const name = cityName.value
    const state = stateName.value
    const url = 'https://localhost:7233/city/add'
    
    const response = await fetch(url, {
      method: "POST",
      headers:  { 'Content-Type': 'application/json' },
      body: JSON.stringify({name, state})
    });
    console.log(response);
  }
</script>

<template>
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

</style>
