<template>
    <div class="col">
        <h1>Groups</h1>
        <table>
            <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Colour</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="contactGroup in contactGroups" :key="contactGroup.id">
                <td>
                {{contactGroup.id}}
                </td>
                <td>
                {{contactGroup.name}}
                </td>
                <td>
                {{contactGroup.contactGroup}}
                </td>
            </tr>
            <tr>
                <td>
                <button @click="onAddContactGroup">
                    +
                </button>
                </td>
                <td>
                <input v-model="newContactGroup.name">
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
  computed: {
    ...mapState(['contactGroups']),
  },
  data() {
    return {
      newContactGroup: {
        name: '',
        colour: '#ffffff',
      }
    }
  },
  methods: {
    ...mapActions(['addContactGroup']),
    onAddContactGroup() {
      this.addContactGroup(this.newContactGroup);
      this.resetNewContactGroup();
    },
    resetNewContactGroup() {
      this.newContactGroup = {
        name: '',
        colour: '#ffffff',
      }
    },
  }
}
</script>