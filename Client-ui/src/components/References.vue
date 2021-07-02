<template>
  <div class="container">
    <h1>References</h1>
    <table>
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Group</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="reference in references" :key="reference.id">
          <td>
            {{reference.id}}
          </td>
          <td>
            {{reference.name}}
          </td>
          <td>
            {{reference.referenceGroup}}
          </td>
        </tr>
        <tr>
          <td>
            <button @click="onAddReference">
              +
            </button>
          </td>
          <td>
            <input v-model="newReference.name">
          </td>
          <td>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'References',
  computed: {
    ...mapState(['references']),
  },
  data() {
    return {
      newReference: {
        name: ''
      }
    }
  },
  methods: {
    ...mapActions(['addReference']),
    onAddReference() {
      this.addReference(this.newReference);
      this.resetNewReference();
    },
    resetNewReference() {
      this.newReference = {
        name: ''
      }
    },
  }
}
</script>