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
            <tr v-for="categoryGroup in categoryGroups" :key="categoryGroup.id">
                <td>
                {{categoryGroup.id}}
                </td>
                <td>
                {{categoryGroup.name}}
                </td>
                <td>
                {{categoryGroup.categoryGroup}}
                </td>
            </tr>
            <tr>
                <td>
                <button @click="onAddCategoryGroup">
                    +
                </button>
                </td>
                <td>
                <input v-model="newCategoryGroup.name">
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
    ...mapState(['categoryGroups']),
  },
  data() {
    return {
      newCategoryGroup: {
        name: '',
        colour: '#ffffff',
      }
    }
  },
  methods: {
    ...mapActions(['addCategoryGroup']),
    onAddCategoryGroup() {
      this.addCategoryGroup(this.newCategoryGroup);
      this.resetNewCategoryGroup();
    },
    resetNewCategoryGroup() {
      this.newCategoryGroup = {
        name: '',
        colour: '#ffffff',
      }
    },
  }
}
</script>