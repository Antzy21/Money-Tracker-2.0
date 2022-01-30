<template>
  <tr>
      <th>{{transaction.id}}</th>
      <th>{{transaction.contact}}</th>
      <th>{{transaction.reference}}</th>
      <th>{{transaction.amount}}</th>
      <th v-if="!showCategoryChoices"
        @click="toggleShowCategoryChoices"
        :class="{ uncategorised: transaction.category==null }">
        {{categoryName}}
      </th>
      <th v-else>
        <select :name="'Category'" @change="onChangeCategory($event)">
          <option value="">{{transaction.Category}}</option>
          <option v-for="category in categories"
            :key="category.id"
            :value="category.id">
            {{category.name}}
          </option>
        </select>
      </th>
  </tr>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'TransactionRow',
  props: {
    transaction: Object,
  },
  computed: {
    ...mapState(['categories']),
    categoryName() {
      if (this.transaction.category) {
        return this.transaction.category.name;
      }
      return this.transaction.Category;
    },
  },
  data() {
    return {
      showCategoryChoices: false,
    }
  },
  methods: {
    ...mapActions(['linkCategoryToTransaction']),
    toggleShowCategoryChoices() {
      this.showCategoryChoices = !this.showCategoryChoices && this.transaction.category == null
    },
    onChangeCategory(event) {
      const value = event.target.value;
      if (value) {
        const category = this.categories.find(o => o.id == value);
        const Category = this.transaction.Category;
        this.linkCategoryToTransaction({ Category, category });
      }
      this.toggleShowCategoryChoices();
    },
  },
}
</script>

<style scoped>
  .uncategorised {
    color: red;
  }
</style>