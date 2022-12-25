<script setup lang="ts">
  import { ref } from 'vue'
  const cityName = ref('')
  const stateName = ref('')
  const createCity = async () => {
    const name = cityName.value
    const state = stateName.value
    const url = 'https://localhost:7233/city/add'
    const response = await fetch(url, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
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
