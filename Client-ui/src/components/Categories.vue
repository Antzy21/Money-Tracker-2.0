<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <h1>Categories</h1>
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Group</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="category in categories" :key="category.id">
              <td>
                {{category.id}}
              </td>
              <td>
                {{category.name}}
              </td>
              <td>
                <select v-model="category.categoryGroupId">
                  <option value="null">None</option>
                  <option v-for="group in categoryGroups"
                    :key="group.id"
                    :value="group.id">
                    {{group.name}}
                  </option>
                </select>
              </td>
            </tr>
            <tr>
              <td>
                <button @click="onAddCategory">
                  +
                </button>
              </td>
              <td>
                <input v-model="newCategory.name">
              </td>
              <td>
                <select v-model="newCategory.categoryGroupId">
                  <option value="null">None</option>
                  <option v-for="group in categoryGroups"
                    :key="group.id"
                    :value="group.id">
                    {{group.name}}
                  </option>
                </select>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'Categories',
  computed: {
    ...mapState(['categories']),
  },
  data() {
    return {
      newCategory: {
        name: '',
        categoryGroupId: null,
      }
    }
  },
  methods: {
    ...mapActions(['addCategory']),
    onAddCategory() {
      this.addCategory(this.newCategory);
      this.resetNewCategory();
    },
    resetNewCategory() {
      this.newCategory = {
        name: '',
        categoryGroupId: null,
      }
    },
  }
}
</script>